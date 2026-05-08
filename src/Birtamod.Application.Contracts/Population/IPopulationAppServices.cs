using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Birtamod.Population;

public interface ICitizenAppService :
    ICrudAppService<
        CitizenDto,
        Guid,
        PagedSortedFilteredRequestDto,
        CreateUpdateCitizenDto>
{
    Task<DateConversionResultDto> ConvertDateAsync(DateConversionInputDto input);
    Task<IRemoteStreamContent> ExportCsvAsync(PagedSortedFilteredRequestDto input);
    Task<IRemoteStreamContent> ExportExcelAsync(PagedSortedFilteredRequestDto input);
    Task BulkDeleteAsync(BulkDeleteInputDto input);
}

public interface IHouseholdAppService :
    ICrudAppService<
        HouseholdDto,
        Guid,
        PagedSortedFilteredRequestDto,
        CreateUpdateHouseholdDto>
{
    Task<IRemoteStreamContent> ExportCsvAsync(PagedSortedFilteredRequestDto input);
    Task<IRemoteStreamContent> ExportExcelAsync(PagedSortedFilteredRequestDto input);
    Task BulkDeleteAsync(BulkDeleteInputDto input);
}

public interface IReligionAppService :
    ICrudAppService<MasterDataDto, Guid, PagedSortedFilteredRequestDto, CreateUpdateMasterDataDto>;
public interface ILanguageAppService :
    ICrudAppService<MasterDataDto, Guid, PagedSortedFilteredRequestDto, CreateUpdateMasterDataDto>;
public interface IEthnicityAppService :
    ICrudAppService<MasterDataDto, Guid, PagedSortedFilteredRequestDto, CreateUpdateMasterDataDto>;
public interface IEducationQualificationAppService :
    ICrudAppService<MasterDataDto, Guid, PagedSortedFilteredRequestDto, CreateUpdateMasterDataDto>;
public interface IFamilyTypeAppService :
    ICrudAppService<MasterDataDto, Guid, PagedSortedFilteredRequestDto, CreateUpdateMasterDataDto>;
public interface IWardAppService :
    ICrudAppService<WardDto, Guid, PagedSortedFilteredRequestDto, CreateUpdateWardDto>;

public interface IDashboardAppService : IApplicationService
{
    Task<DashboardDataDto> GetAdminDashboardAsync();
    Task<DashboardDataDto> GetPublicDashboardAsync(PagedSortedFilteredRequestDto filter);
}
