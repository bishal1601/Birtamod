using Birtamod.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Birtamod.Permissions;

public class BirtamodPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BirtamodPermissions.GroupName);
        AddCrudPermissions(myGroup, BirtamodPermissions.Citizens.Default, "Permission:Citizens", true);
        AddCrudPermissions(myGroup, BirtamodPermissions.Households.Default, "Permission:Households", true);
        AddCrudPermissions(myGroup, BirtamodPermissions.Religions.Default, "Permission:Religions");
        AddCrudPermissions(myGroup, BirtamodPermissions.Languages.Default, "Permission:Languages");
        AddCrudPermissions(myGroup, BirtamodPermissions.Ethnicities.Default, "Permission:Ethnicities");
        AddCrudPermissions(myGroup, BirtamodPermissions.EducationQualifications.Default, "Permission:EducationQualifications");
        AddCrudPermissions(myGroup, BirtamodPermissions.FamilyTypes.Default, "Permission:FamilyTypes");
        AddCrudPermissions(myGroup, BirtamodPermissions.Wards.Default, "Permission:Wards");

        var dashboard = myGroup.AddPermission(BirtamodPermissions.Dashboard.Default, L("Permission:Dashboard"));
        dashboard.AddChild(BirtamodPermissions.Dashboard.View, L("Permission:Dashboard.View"));
        dashboard.AddChild(BirtamodPermissions.Dashboard.PublicView, L("Permission:Dashboard.PublicView"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BirtamodResource>(name);
    }

    private static void AddCrudPermissions(
        PermissionGroupDefinition group,
        string defaultPermission,
        string displayName,
        bool includeExportPermissions = false)
    {
        var permission = group.AddPermission(defaultPermission, L(displayName));
        permission.AddChild(defaultPermission + ".Create", L(displayName + ".Create"));
        permission.AddChild(defaultPermission + ".Edit", L(displayName + ".Edit"));
        permission.AddChild(defaultPermission + ".Delete", L(displayName + ".Delete"));

        if (includeExportPermissions)
        {
            permission.AddChild(defaultPermission + ".Export", L(displayName + ".Export"));
            permission.AddChild(defaultPermission + ".BulkDelete", L(displayName + ".BulkDelete"));
        }
    }
}
