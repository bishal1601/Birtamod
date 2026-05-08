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

        //Define your own permissions here. Example:
        //myGroup.AddPermission(BirtamodPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BirtamodResource>(name);
    }
}
