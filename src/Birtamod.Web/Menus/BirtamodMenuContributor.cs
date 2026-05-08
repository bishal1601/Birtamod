using System.Threading.Tasks;
using Birtamod.Localization;
using Birtamod.Permissions;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;

namespace Birtamod.Web.Menus;

public class BirtamodMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<BirtamodResource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                BirtamodMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                BirtamodMenus.Dashboard,
                l["Menu:Dashboard"],
                "~/Dashboard",
                icon: "fa fa-chart-line",
                order: 2
            ).RequirePermissions(BirtamodPermissions.Dashboard.View)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                BirtamodMenus.PublicDashboard,
                l["Menu:PublicDashboard"],
                "~/PublicDashboard",
                icon: "fa fa-globe",
                order: 3
            )
        );

        var population = new ApplicationMenuItem(
            BirtamodMenus.PopulationManagement,
            l["Menu:PopulationManagement"],
            icon: "fa fa-users",
            order: 4
        );
        population.AddItem(new ApplicationMenuItem("Birtamod.Citizens", l["Menu:Citizens"], "~/Citizens").RequirePermissions(BirtamodPermissions.Citizens.Default));
        population.AddItem(new ApplicationMenuItem("Birtamod.Households", l["Menu:Households"], "~/Households").RequirePermissions(BirtamodPermissions.Households.Default));
        population.AddItem(new ApplicationMenuItem("Birtamod.Religions", l["Menu:Religions"], "~/Religions").RequirePermissions(BirtamodPermissions.Religions.Default));
        population.AddItem(new ApplicationMenuItem("Birtamod.Languages", l["Menu:Languages"], "~/Languages").RequirePermissions(BirtamodPermissions.Languages.Default));
        population.AddItem(new ApplicationMenuItem("Birtamod.Ethnicities", l["Menu:Ethnicities"], "~/Ethnicities").RequirePermissions(BirtamodPermissions.Ethnicities.Default));
        population.AddItem(new ApplicationMenuItem("Birtamod.EducationQualifications", l["Menu:EducationQualifications"], "~/EducationQualifications").RequirePermissions(BirtamodPermissions.EducationQualifications.Default));
        population.AddItem(new ApplicationMenuItem("Birtamod.FamilyTypes", l["Menu:FamilyTypes"], "~/FamilyTypes").RequirePermissions(BirtamodPermissions.FamilyTypes.Default));
        population.AddItem(new ApplicationMenuItem("Birtamod.Wards", l["Menu:Wards"], "~/Wards").RequirePermissions(BirtamodPermissions.Wards.Default));
        context.Menu.AddItem(population);


        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 6;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);
    
        administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 7);
        
        return Task.CompletedTask;
    }
}
