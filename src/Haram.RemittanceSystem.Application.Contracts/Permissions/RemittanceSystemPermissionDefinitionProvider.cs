using Haram.RemittanceSystem.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Haram.RemittanceSystem.Permissions;

public class RemittanceSystemPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(RemittanceSystemPermissions.GroupName);
        //Define your own permissions here. Example:
        myGroup.AddPermission(RemittanceSystemPermissions.Remittances.Default, L("Permission:Remittances")).AddChild(RemittanceSystemPermissions.Remittances.Create);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<RemittanceSystemResource>(name);
    }
}
