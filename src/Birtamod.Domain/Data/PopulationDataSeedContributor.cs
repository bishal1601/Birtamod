using System;
using System.Linq;
using System.Threading.Tasks;
using Birtamod.Population;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Birtamod.Data;

public class PopulationDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private const string GroupName = "Birtamod";
    private const string Citizens = GroupName + ".Citizens";
    private const string Households = GroupName + ".Households";
    private const string DashboardView = GroupName + ".Dashboard.View";
    private const string DashboardPublicView = GroupName + ".Dashboard.PublicView";
    private const string Religions = GroupName + ".Religions";
    private const string Languages = GroupName + ".Languages";
    private const string Ethnicities = GroupName + ".Ethnicities";
    private const string EducationQualifications = GroupName + ".EducationQualifications";
    private const string FamilyTypes = GroupName + ".FamilyTypes";
    private const string Wards = GroupName + ".Wards";
    private readonly IdentityRoleManager _roleManager;
    private readonly IPermissionManager _permissionManager;
    private readonly IRepository<Religion, Guid> _religionRepository;
    private readonly IRepository<Language, Guid> _languageRepository;
    private readonly IRepository<Ethnicity, Guid> _ethnicityRepository;
    private readonly IRepository<EducationQualification, Guid> _educationRepository;
    private readonly IRepository<FamilyType, Guid> _familyTypeRepository;
    private readonly IRepository<Ward, Guid> _wardRepository;

    public PopulationDataSeedContributor(
        IdentityRoleManager roleManager,
        IPermissionManager permissionManager,
        IRepository<Religion, Guid> religionRepository,
        IRepository<Language, Guid> languageRepository,
        IRepository<Ethnicity, Guid> ethnicityRepository,
        IRepository<EducationQualification, Guid> educationRepository,
        IRepository<FamilyType, Guid> familyTypeRepository,
        IRepository<Ward, Guid> wardRepository)
    {
        _roleManager = roleManager;
        _permissionManager = permissionManager;
        _religionRepository = religionRepository;
        _languageRepository = languageRepository;
        _ethnicityRepository = ethnicityRepository;
        _educationRepository = educationRepository;
        _familyTypeRepository = familyTypeRepository;
        _wardRepository = wardRepository;
    }

    [UnitOfWork]
    public async Task SeedAsync(DataSeedContext context)
    {
        await SeedRoleWithPermissionsAsync("Super Admin", [GroupName + ".*"]);
        await SeedRoleWithPermissionsAsync("Municipality Admin",
        [
            Citizens, Households, DashboardView,
            Religions, Languages, Ethnicities,
            EducationQualifications, FamilyTypes, Wards
        ]);
        await SeedRoleWithPermissionsAsync("Data Entry",
        [
            Citizens, Citizens + ".Create", Citizens + ".Edit",
            Households, Households + ".Create", Households + ".Edit"
        ]);
        await SeedRoleWithPermissionsAsync("Viewer",
        [
            Citizens, Households,
            DashboardView, DashboardPublicView
        ]);

        await SeedMasterDataAsync();
    }

    private async Task SeedRoleWithPermissionsAsync(string roleName, string[] permissions)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            role = new IdentityRole(Guid.NewGuid(), roleName);
            var result = await _roleManager.CreateAsync(role);
            result.CheckErrors();
        }

        if (permissions.Contains(GroupName + ".*"))
        {
            // Super admin gets everything by the ABP role management UI later.
            return;
        }

        foreach (var permission in permissions)
        {
            await _permissionManager.SetForRoleAsync(roleName, permission, true);
        }
    }

    private async Task SeedMasterDataAsync()
    {
        if (!await _religionRepository.AnyAsync())
        {
            await _religionRepository.InsertManyAsync(
            [
                new Religion { Name = "Hindu", IsActive = true },
                new Religion { Name = "Buddhist", IsActive = true },
                new Religion { Name = "Kirat", IsActive = true },
                new Religion { Name = "Christian", IsActive = true },
                new Religion { Name = "Muslim", IsActive = true }
            ], true);
        }

        if (!await _languageRepository.AnyAsync())
        {
            await _languageRepository.InsertManyAsync(
            [
                new Language { Name = "Nepali", IsActive = true },
                new Language { Name = "Maithili", IsActive = true },
                new Language { Name = "Limbu", IsActive = true },
                new Language { Name = "Rai", IsActive = true }
            ], true);
        }

        if (!await _ethnicityRepository.AnyAsync())
        {
            await _ethnicityRepository.InsertManyAsync(
            [
                new Ethnicity { Name = "Brahmin", IsActive = true },
                new Ethnicity { Name = "Chhetri", IsActive = true },
                new Ethnicity { Name = "Janajati", IsActive = true },
                new Ethnicity { Name = "Dalit", IsActive = true }
            ], true);
        }

        if (!await _educationRepository.AnyAsync())
        {
            await _educationRepository.InsertManyAsync(
            [
                new EducationQualification { Name = "Illiterate", IsActive = true },
                new EducationQualification { Name = "Primary", IsActive = true },
                new EducationQualification { Name = "Secondary", IsActive = true },
                new EducationQualification { Name = "Bachelor", IsActive = true }
            ], true);
        }

        if (!await _familyTypeRepository.AnyAsync())
        {
            await _familyTypeRepository.InsertManyAsync(
            [
                new FamilyType { Name = "Nuclear", IsActive = true },
                new FamilyType { Name = "Joint", IsActive = true }
            ], true);
        }

        if (!await _wardRepository.AnyAsync())
        {
            for (var i = 1; i <= 10; i++)
            {
                await _wardRepository.InsertAsync(new Ward
                {
                    WardNumber = i,
                    WardName = $"Ward {i}",
                    Description = "Auto-seeded sample ward"
                }, true);
            }
        }
    }
}
