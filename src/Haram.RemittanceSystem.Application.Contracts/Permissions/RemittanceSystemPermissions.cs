namespace Haram.RemittanceSystem.Permissions;

public static class RemittanceSystemPermissions
{
    public const string GroupName = "RemittanceSystem";

    
    public static class Remittances
    {
        public const string Default = GroupName + ".Remittances";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string SetReady = Default + ".SetReady";
        public const string SetApproved = Default + ".SetApproved";
        public const string SetReleased = Default + ".SetReleased";

    }
    public static class Customers
    {
        public const string Default = GroupName + ".Customers";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Currencies
    {
        public const string Default = GroupName + ".Currencies";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
