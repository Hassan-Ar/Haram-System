using Haram.RemittanceSystem.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Haram.RemittanceSystem.Permissions;

public class RemittanceSystemPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {

        var Group = context.AddGroup(RemittanceSystemPermissions.GroupName, L("Permission:RemittancesSystem"));

        var RemittancePermission = Group.AddPermission(RemittanceSystemPermissions.Remittances.Default, L("Permission:Remittances Management"));
        RemittancePermission.AddChild(RemittanceSystemPermissions.Remittances.Create, L("Permission:Create"));
        RemittancePermission.AddChild(RemittanceSystemPermissions.Remittances.Edit, L("Permission:Edit"));
        RemittancePermission.AddChild(RemittanceSystemPermissions.Remittances.Delete, L("Permission:Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<RemittanceSystemResource>(name);
    }
}
