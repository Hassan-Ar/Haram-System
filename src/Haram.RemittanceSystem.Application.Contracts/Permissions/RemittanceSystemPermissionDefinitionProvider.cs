using Haram.RemittanceSystem.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Haram.RemittanceSystem.Permissions;

public class RemittanceSystemPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var Group = context.AddGroup(RemittanceSystemPermissions.GroupName, L("Permission:RemittanceSystem"));


        var remittancePermission = Group.AddPermission(
    RemittanceSystemPermissions.Remittances.Default, L("Permission:Remittances"));
        remittancePermission.AddChild(
            RemittanceSystemPermissions.Remittances.Create, L("Permission:Remittances.Create"));
        remittancePermission.AddChild(
            RemittanceSystemPermissions.Remittances.Edit, L("Permission:Remittances.Edit"));
        remittancePermission.AddChild(
            RemittanceSystemPermissions.Remittances.Delete, L("Permission:Remittances.Delete"));

        remittancePermission.AddChild(
            RemittanceSystemPermissions.Remittances.SetReady, L("Permission:Remittances.SetReady"));
        remittancePermission.AddChild(
    RemittanceSystemPermissions.Remittances.SetApproved, L("Permission:Remittances.SetApproved"));
        remittancePermission.AddChild(
RemittanceSystemPermissions.Remittances.SetReleased, L("Permission:Remittances.SetReleased"));


    var currencyPermission = Group.AddPermission(
RemittanceSystemPermissions.Currencies.Default, L("Permission:Currencies"));
    currencyPermission.AddChild(
        RemittanceSystemPermissions.Currencies.Create, L("Permission:Currencies.Create"));
        currencyPermission.AddChild(
            RemittanceSystemPermissions.Currencies.Edit, L("Permission:Currencies.Edit"));
        currencyPermission.AddChild(
            RemittanceSystemPermissions.Currencies.Delete, L("Permission:Currencies.Delete"));

        var CustomerPermission = Group.AddPermission(
RemittanceSystemPermissions.Customers.Default, L("Permission:Customers"));
    CustomerPermission.AddChild(
        RemittanceSystemPermissions.Customers.Create, L("Permission:Customers.Create"));
        CustomerPermission.AddChild(
            RemittanceSystemPermissions.Customers.Edit, L("Permission:Customers.Edit"));
        CustomerPermission.AddChild(
            RemittanceSystemPermissions.Customers.Delete, L("Permission:Customers.Delete"));

    }

private static LocalizableString L(string name)
{
    return LocalizableString.Create<RemittanceSystemResource>(name);
}
}
