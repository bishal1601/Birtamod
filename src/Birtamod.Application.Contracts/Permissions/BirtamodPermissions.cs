namespace Birtamod.Permissions;

public static class BirtamodPermissions
{
    public const string GroupName = "Birtamod";

    public static class Citizens
    {
        public const string Default = GroupName + ".Citizens";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Export = Default + ".Export";
        public const string BulkDelete = Default + ".BulkDelete";
    }

    public static class Households
    {
        public const string Default = GroupName + ".Households";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Export = Default + ".Export";
        public const string BulkDelete = Default + ".BulkDelete";
    }

    public static class Religions
    {
        public const string Default = GroupName + ".Religions";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Languages
    {
        public const string Default = GroupName + ".Languages";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Ethnicities
    {
        public const string Default = GroupName + ".Ethnicities";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class EducationQualifications
    {
        public const string Default = GroupName + ".EducationQualifications";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class FamilyTypes
    {
        public const string Default = GroupName + ".FamilyTypes";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Wards
    {
        public const string Default = GroupName + ".Wards";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Dashboard
    {
        public const string Default = GroupName + ".Dashboard";
        public const string View = Default + ".View";
        public const string PublicView = Default + ".PublicView";
    }
}
