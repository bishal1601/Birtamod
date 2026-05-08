using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;
using Volo.Abp.ObjectMapping;
using Birtamod.Permissions;

namespace Birtamod.Population;

public abstract class MasterDataAppService<TEntity> :
    BirtamodAppService,
    ICrudAppService<MasterDataDto, Guid, PagedSortedFilteredRequestDto, CreateUpdateMasterDataDto>
    where TEntity : MasterDataEntity, new()
{
    protected readonly IRepository<TEntity, Guid> Repository;

    protected MasterDataAppService(IRepository<TEntity, Guid> repository)
    {
        Repository = repository;
    }

    public virtual async Task<PagedResultDto<MasterDataDto>> GetListAsync(PagedSortedFilteredRequestDto input)
    {
        var queryable = await Repository.GetQueryableAsync();
        queryable = queryable
            .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Filter!));

        var totalCount = await AsyncExecuter.CountAsync(queryable);
        var entities = await AsyncExecuter.ToListAsync(queryable
            .OrderBy(x => x.Name)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount));

        return new PagedResultDto<MasterDataDto>(
            totalCount,
            entities.Select(ObjectMapper.Map<TEntity, MasterDataDto>).ToList());
    }

    public virtual async Task<MasterDataDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<TEntity, MasterDataDto>(await Repository.GetAsync(id));
    }

    public virtual async Task<MasterDataDto> CreateAsync(CreateUpdateMasterDataDto input)
    {
        var entity = ObjectMapper.Map<CreateUpdateMasterDataDto, TEntity>(input);
        await Repository.InsertAsync(entity, true);
        return ObjectMapper.Map<TEntity, MasterDataDto>(entity);
    }

    public virtual async Task<MasterDataDto> UpdateAsync(Guid id, CreateUpdateMasterDataDto input)
    {
        var entity = await Repository.GetAsync(id);
        ObjectMapper.Map(input, entity);
        await Repository.UpdateAsync(entity, true);
        return ObjectMapper.Map<TEntity, MasterDataDto>(entity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await Repository.DeleteAsync(id);
    }
}

[Authorize(BirtamodPermissions.Citizens.Default)]
public class CitizenAppService :
    BirtamodAppService,
    ICitizenAppService
{
    private readonly IRepository<Citizen, Guid> _citizenRepository;
    private readonly IDateConversionManager _dateConversionManager;
    private readonly IAsyncQueryableExecuter _asyncExecuter;

    public CitizenAppService(
        IRepository<Citizen, Guid> citizenRepository,
        IDateConversionManager dateConversionManager,
        IAsyncQueryableExecuter asyncExecuter)
    {
        _citizenRepository = citizenRepository;
        _dateConversionManager = dateConversionManager;
        _asyncExecuter = asyncExecuter;
    }

    public async Task<PagedResultDto<CitizenDto>> GetListAsync(PagedSortedFilteredRequestDto input)
    {
        var queryable = await _citizenRepository.GetQueryableAsync();
        queryable = queryable.WhereIf(!input.Filter.IsNullOrWhiteSpace(),
            x => x.FirstName.Contains(input.Filter!) ||
                 x.LastName.Contains(input.Filter!) ||
                 (x.CitizenshipNumber ?? string.Empty).Contains(input.Filter!));

        var count = await _asyncExecuter.CountAsync(queryable);
        var list = await _asyncExecuter.ToListAsync(queryable
            .OrderBy(x => x.FirstName)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount));

        return new PagedResultDto<CitizenDto>(count, ObjectMapper.Map<List<Citizen>, List<CitizenDto>>(list));
    }

    public async Task<CitizenDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<Citizen, CitizenDto>(await _citizenRepository.GetAsync(id));
    }

    [Authorize(BirtamodPermissions.Citizens.Create)]
    public async Task<CitizenDto> CreateAsync(CreateUpdateCitizenDto input)
    {
        var entity = new Citizen();
        await FillDobFieldsAsync(input, entity);
        ObjectMapper.Map(input, entity);
        await _citizenRepository.InsertAsync(entity, true);
        return ObjectMapper.Map<Citizen, CitizenDto>(entity);
    }

    [Authorize(BirtamodPermissions.Citizens.Edit)]
    public async Task<CitizenDto> UpdateAsync(Guid id, CreateUpdateCitizenDto input)
    {
        var entity = await _citizenRepository.GetAsync(id);
        await FillDobFieldsAsync(input, entity);
        ObjectMapper.Map(input, entity);
        await _citizenRepository.UpdateAsync(entity, true);
        return ObjectMapper.Map<Citizen, CitizenDto>(entity);
    }

    [Authorize(BirtamodPermissions.Citizens.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _citizenRepository.DeleteAsync(id);
    }

    [Authorize(BirtamodPermissions.Citizens.BulkDelete)]
    public async Task BulkDeleteAsync(BulkDeleteInputDto input)
    {
        await _citizenRepository.DeleteManyAsync(input.Ids);
    }

    [Authorize(BirtamodPermissions.Citizens.Default)]
    public Task<DateConversionResultDto> ConvertDateAsync(DateConversionInputDto input)
    {
        if (input.DateOfBirthAd is null && input.DateOfBirthBs.IsNullOrWhiteSpace())
        {
            throw new UserFriendlyException(L["DateOfBirthRequired"]);
        }

        DateTime adDate;
        string bsDate;
        if (input.DateOfBirthAd.HasValue)
        {
            adDate = input.DateOfBirthAd.Value.Date;
            bsDate = _dateConversionManager.ConvertAdToBs(adDate);
        }
        else
        {
            bsDate = input.DateOfBirthBs!;
            adDate = _dateConversionManager.ConvertBsToAd(bsDate);
        }

        var age = _dateConversionManager.CalculateAge(adDate);

        return Task.FromResult(new DateConversionResultDto
        {
            DateOfBirthAd = adDate,
            DateOfBirthBs = bsDate,
            Age = age
        });
    }

    [Authorize(BirtamodPermissions.Citizens.Export)]
    public async Task<IRemoteStreamContent> ExportCsvAsync(PagedSortedFilteredRequestDto input)
    {
        var items = (await GetListAsync(input)).Items;
        var sb = new StringBuilder();
        sb.AppendLine("FirstName,LastName,Gender,DOB_AD,DOB_BS,Age,CitizenshipNumber,PhoneNumber");
        foreach (var item in items)
        {
            sb.AppendLine($"{item.FirstName},{item.LastName},{item.Gender},{item.DateOfBirthAd:yyyy-MM-dd},{item.DateOfBirthBs},{item.Age},{item.CitizenshipNumber},{item.PhoneNumber}");
        }

        return new RemoteStreamContent(
            new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString())),
            "citizens.csv",
            "text/csv");
    }

    [Authorize(BirtamodPermissions.Citizens.Export)]
    public async Task<IRemoteStreamContent> ExportExcelAsync(PagedSortedFilteredRequestDto input)
    {
        // CSV format with xls extension for lightweight compatibility.
        var csv = await ExportCsvAsync(input);
        return new RemoteStreamContent(csv.GetStream(), "citizens.xls", "application/vnd.ms-excel");
    }

    private async Task FillDobFieldsAsync(CreateUpdateCitizenDto input, Citizen entity)
    {
        var result = await ConvertDateAsync(new DateConversionInputDto
        {
            DateOfBirthAd = input.DateOfBirthAd,
            DateOfBirthBs = input.DateOfBirthBs
        });
        entity.DateOfBirthAd = result.DateOfBirthAd;
        entity.DateOfBirthBs = result.DateOfBirthBs;
        entity.Age = result.Age;
    }
}

[Authorize(BirtamodPermissions.Households.Default)]
public class HouseholdAppService :
    BirtamodAppService,
    IHouseholdAppService
{
    private readonly IRepository<Household, Guid> _repository;
    private readonly IAsyncQueryableExecuter _asyncExecuter;

    public HouseholdAppService(IRepository<Household, Guid> repository, IAsyncQueryableExecuter asyncExecuter)
    {
        _repository = repository;
        _asyncExecuter = asyncExecuter;
    }

    public async Task<PagedResultDto<HouseholdDto>> GetListAsync(PagedSortedFilteredRequestDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        queryable = queryable.WhereIf(!input.Filter.IsNullOrWhiteSpace(),
            x => x.HouseNumber.Contains(input.Filter!) || x.FamilyHeadName.Contains(input.Filter!));

        var count = await _asyncExecuter.CountAsync(queryable);
        var list = await _asyncExecuter.ToListAsync(queryable
            .OrderBy(x => x.HouseNumber)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount));

        return new PagedResultDto<HouseholdDto>(count, ObjectMapper.Map<List<Household>, List<HouseholdDto>>(list));
    }

    public async Task<HouseholdDto> GetAsync(Guid id) => ObjectMapper.Map<Household, HouseholdDto>(await _repository.GetAsync(id));

    [Authorize(BirtamodPermissions.Households.Create)]
    public async Task<HouseholdDto> CreateAsync(CreateUpdateHouseholdDto input)
    {
        var entity = ObjectMapper.Map<CreateUpdateHouseholdDto, Household>(input);
        await _repository.InsertAsync(entity, true);
        return ObjectMapper.Map<Household, HouseholdDto>(entity);
    }

    [Authorize(BirtamodPermissions.Households.Edit)]
    public async Task<HouseholdDto> UpdateAsync(Guid id, CreateUpdateHouseholdDto input)
    {
        var entity = await _repository.GetAsync(id);
        ObjectMapper.Map(input, entity);
        await _repository.UpdateAsync(entity, true);
        return ObjectMapper.Map<Household, HouseholdDto>(entity);
    }

    [Authorize(BirtamodPermissions.Households.Delete)]
    public Task DeleteAsync(Guid id) => _repository.DeleteAsync(id);

    [Authorize(BirtamodPermissions.Households.BulkDelete)]
    public Task BulkDeleteAsync(BulkDeleteInputDto input) => _repository.DeleteManyAsync(input.Ids);

    [Authorize(BirtamodPermissions.Households.Export)]
    public async Task<IRemoteStreamContent> ExportCsvAsync(PagedSortedFilteredRequestDto input)
    {
        var items = (await GetListAsync(input)).Items;
        var sb = new StringBuilder();
        sb.AppendLine("HouseNumber,FamilyHeadName,TotalMembers,Address");
        foreach (var item in items)
        {
            sb.AppendLine($"{item.HouseNumber},{item.FamilyHeadName},{item.TotalMembers},{item.Address}");
        }

        return new RemoteStreamContent(new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString())), "households.csv", "text/csv");
    }

    [Authorize(BirtamodPermissions.Households.Export)]
    public async Task<IRemoteStreamContent> ExportExcelAsync(PagedSortedFilteredRequestDto input)
    {
        var csv = await ExportCsvAsync(input);
        return new RemoteStreamContent(csv.GetStream(), "households.xls", "application/vnd.ms-excel");
    }
}

[Authorize(BirtamodPermissions.Religions.Default)]
public class ReligionAppService : MasterDataAppService<Religion>, IReligionAppService
{
    public ReligionAppService(IRepository<Religion, Guid> repository) : base(repository) { }
}

[Authorize(BirtamodPermissions.Languages.Default)]
public class LanguageAppService : MasterDataAppService<Language>, ILanguageAppService
{
    public LanguageAppService(IRepository<Language, Guid> repository) : base(repository) { }
}

[Authorize(BirtamodPermissions.Ethnicities.Default)]
public class EthnicityAppService : MasterDataAppService<Ethnicity>, IEthnicityAppService
{
    public EthnicityAppService(IRepository<Ethnicity, Guid> repository) : base(repository) { }
}

[Authorize(BirtamodPermissions.EducationQualifications.Default)]
public class EducationQualificationAppService : MasterDataAppService<EducationQualification>, IEducationQualificationAppService
{
    public EducationQualificationAppService(IRepository<EducationQualification, Guid> repository) : base(repository) { }
}

[Authorize(BirtamodPermissions.FamilyTypes.Default)]
public class FamilyTypeAppService : MasterDataAppService<FamilyType>, IFamilyTypeAppService
{
    public FamilyTypeAppService(IRepository<FamilyType, Guid> repository) : base(repository) { }
}

[Authorize(BirtamodPermissions.Wards.Default)]
public class WardAppService :
    BirtamodAppService,
    IWardAppService
{
    private readonly IRepository<Ward, Guid> _repository;
    private readonly IAsyncQueryableExecuter _asyncExecuter;

    public WardAppService(IRepository<Ward, Guid> repository, IAsyncQueryableExecuter asyncExecuter)
    {
        _repository = repository;
        _asyncExecuter = asyncExecuter;
    }

    public async Task<PagedResultDto<WardDto>> GetListAsync(PagedSortedFilteredRequestDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        queryable = queryable.WhereIf(!input.Filter.IsNullOrWhiteSpace(),
            x => x.WardName.Contains(input.Filter!) || x.WardNumber.ToString().Contains(input.Filter!));

        var count = await _asyncExecuter.CountAsync(queryable);
        var list = await _asyncExecuter.ToListAsync(queryable
            .OrderBy(x => x.WardNumber)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount));

        return new PagedResultDto<WardDto>(count, ObjectMapper.Map<List<Ward>, List<WardDto>>(list));
    }

    public async Task<WardDto> GetAsync(Guid id) => ObjectMapper.Map<Ward, WardDto>(await _repository.GetAsync(id));

    [Authorize(BirtamodPermissions.Wards.Create)]
    public async Task<WardDto> CreateAsync(CreateUpdateWardDto input)
    {
        var entity = ObjectMapper.Map<CreateUpdateWardDto, Ward>(input);
        await _repository.InsertAsync(entity, true);
        return ObjectMapper.Map<Ward, WardDto>(entity);
    }

    [Authorize(BirtamodPermissions.Wards.Edit)]
    public async Task<WardDto> UpdateAsync(Guid id, CreateUpdateWardDto input)
    {
        var entity = await _repository.GetAsync(id);
        ObjectMapper.Map(input, entity);
        await _repository.UpdateAsync(entity, true);
        return ObjectMapper.Map<Ward, WardDto>(entity);
    }

    [Authorize(BirtamodPermissions.Wards.Delete)]
    public Task DeleteAsync(Guid id) => _repository.DeleteAsync(id);
}

[AllowAnonymous]
public class DashboardAppService : BirtamodAppService, IDashboardAppService
{
    private readonly IRepository<Citizen, Guid> _citizens;
    private readonly IRepository<Household, Guid> _households;
    private readonly IRepository<Religion, Guid> _religions;
    private readonly IRepository<Language, Guid> _languages;
    private readonly IRepository<Ethnicity, Guid> _ethnicities;
    private readonly IRepository<EducationQualification, Guid> _educationQualifications;
    private readonly IRepository<FamilyType, Guid> _familyTypes;
    private readonly IRepository<Ward, Guid> _wards;
    private readonly IAsyncQueryableExecuter _asyncExecuter;

    public DashboardAppService(
        IRepository<Citizen, Guid> citizens,
        IRepository<Household, Guid> households,
        IRepository<Religion, Guid> religions,
        IRepository<Language, Guid> languages,
        IRepository<Ethnicity, Guid> ethnicities,
        IRepository<EducationQualification, Guid> educationQualifications,
        IRepository<FamilyType, Guid> familyTypes,
        IRepository<Ward, Guid> wards,
        IAsyncQueryableExecuter asyncExecuter)
    {
        _citizens = citizens;
        _households = households;
        _religions = religions;
        _languages = languages;
        _ethnicities = ethnicities;
        _educationQualifications = educationQualifications;
        _familyTypes = familyTypes;
        _wards = wards;
        _asyncExecuter = asyncExecuter;
    }

    [Authorize(BirtamodPermissions.Dashboard.View)]
    public Task<DashboardDataDto> GetAdminDashboardAsync() => BuildDashboardDataAsync(null);

    [Authorize(BirtamodPermissions.Dashboard.PublicView)]
    public Task<DashboardDataDto> GetPublicDashboardAsync(PagedSortedFilteredRequestDto filter) => BuildDashboardDataAsync(filter.Filter);

    private async Task<DashboardDataDto> BuildDashboardDataAsync(string? wardFilter)
    {
        var citizenQueryable = await _citizens.GetQueryableAsync();
        var householdQueryable = await _households.GetQueryableAsync();

        if (!wardFilter.IsNullOrWhiteSpace() && int.TryParse(wardFilter, out var wardNo))
        {
            var wardQueryable = await _wards.GetQueryableAsync();
            var wardId = await _asyncExecuter.FirstOrDefaultAsync(wardQueryable.Where(x => x.WardNumber == wardNo).Select(x => x.Id));
            if (wardId != default)
            {
                citizenQueryable = citizenQueryable.Where(x => x.WardId == wardId);
                householdQueryable = householdQueryable.Where(x => x.WardId == wardId);
            }
        }

        var citizens = await _asyncExecuter.ToListAsync(citizenQueryable);
        var households = await _asyncExecuter.ToListAsync(householdQueryable);

        var result = new DashboardDataDto
        {
            Summary = new DashboardSummaryDto
            {
                TotalHouseholds = households.Count,
                TotalPopulation = citizens.Count,
                TotalMale = citizens.Count(x => x.Gender == Gender.Male),
                TotalFemale = citizens.Count(x => x.Gender == Gender.Female),
                TotalOtherGender = citizens.Count(x => x.Gender == Gender.Other)
            }
        };

        result.PopulationByGender = citizens.GroupBy(x => x.Gender.ToString())
            .Select(x => new StatisticItemDto { Name = x.Key, Count = x.Count() }).ToList();
        result.PopulationByDisability = ToBoolStat(citizens.Select(x => x.DisabilityStatus), "With Disability", "Without Disability");
        result.PopulationByToiletAvailability = ToBoolStat(citizens.Select(x => x.HasToilet), "Has Toilet", "No Toilet");
        result.PopulationByHouseOwnership = ToBoolStat(citizens.Select(x => x.IsHouseOwner), "House Owner", "Not Owner");
        result.PopulationByAgeGroup = citizens.GroupBy(x => GetAgeGroup(x.Age))
            .Select(x => new StatisticItemDto { Name = x.Key, Count = x.Count() })
            .OrderBy(x => x.Name)
            .ToList();

        result.PopulationByReligion = await GroupFromMasterDataAsync(citizens.Select(x => x.ReligionId).ToList(), _religions);
        result.PopulationByLanguage = await GroupFromMasterDataAsync(citizens.Select(x => x.LanguageId).ToList(), _languages);
        result.PopulationByEthnicity = await GroupFromMasterDataAsync(citizens.Select(x => x.EthnicityId).ToList(), _ethnicities);
        result.PopulationByEducation = await GroupFromMasterDataAsync(citizens.Select(x => x.EducationQualificationId).ToList(), _educationQualifications);
        result.PopulationByFamilyType = await GroupFromMasterDataAsync(citizens.Select(x => x.FamilyTypeId).ToList(), _familyTypes);
        result.WardWisePopulation = await GroupFromWardAsync(citizens.Select(x => x.WardId).ToList());

        return result;
    }

    private async Task<List<StatisticItemDto>> GroupFromMasterDataAsync<TEntity>(List<Guid> ids, IRepository<TEntity, Guid> repository)
        where TEntity : MasterDataEntity
    {
        var items = await _asyncExecuter.ToListAsync(await repository.GetQueryableAsync());
        return ids.GroupBy(x => x)
            .Select(x => new StatisticItemDto
            {
                Name = items.FirstOrDefault(i => i.Id == x.Key)?.Name ?? "Unknown",
                Count = x.Count()
            })
            .OrderByDescending(x => x.Count)
            .ToList();
    }

    private async Task<List<StatisticItemDto>> GroupFromWardAsync(List<Guid> ids)
    {
        var items = await _asyncExecuter.ToListAsync(await _wards.GetQueryableAsync());
        return ids.GroupBy(x => x)
            .Select(x => new StatisticItemDto
            {
                Name = items.FirstOrDefault(i => i.Id == x.Key)?.WardName ?? "Unknown",
                Count = x.Count()
            })
            .OrderBy(x => x.Name)
            .ToList();
    }

    private static List<StatisticItemDto> ToBoolStat(IEnumerable<bool> values, string trueLabel, string falseLabel)
    {
        var grouped = values.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        return
        [
            new StatisticItemDto { Name = trueLabel, Count = grouped.GetValueOrDefault(true) },
            new StatisticItemDto { Name = falseLabel, Count = grouped.GetValueOrDefault(false) }
        ];
    }

    private static string GetAgeGroup(int age)
    {
        if (age < 18) return "0-17";
        if (age < 30) return "18-29";
        if (age < 45) return "30-44";
        if (age < 60) return "45-59";
        return "60+";
    }
}
