using True.TECH.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace True.TECH.Permissions;

public class TECHPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TECHPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(TECHPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TECHResource>(name);
    }
}
