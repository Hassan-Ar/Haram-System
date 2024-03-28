namespace Haram.RemittanceSystem.Permissions;

public static class RemittanceSystemPermissions
{
    public const string GroupName = "RemittanceSystem";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public static class Remittances
    {
        public const string Default = ".Remittances";
        public const string Create = Default + ".Create";
    }
}
