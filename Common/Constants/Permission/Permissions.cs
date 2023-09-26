using System.ComponentModel;
using System.Reflection;

namespace Common.Constants.Permission
{
    public static class Permissions
    {
        [DisplayName("Users")]
        [Description("Users Permissions")]
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
            public const string Export = "Permissions.Users.Export";
            public const string Search = "Permissions.Users.Search";
        }

        [DisplayName("Roles")]
        [Description("Roles Permissions")]
        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
            public const string Search = "Permissions.Roles.Search";
        }

        [DisplayName("Role Claims")]
        [Description("Role Claims Permissions")]
        public static class RoleClaims
        {
            public const string View = "Permissions.RoleClaims.View";
            public const string Create = "Permissions.RoleClaims.Create";
            public const string Edit = "Permissions.RoleClaims.Edit";
            public const string Delete = "Permissions.RoleClaims.Delete";
            public const string Search = "Permissions.RoleClaims.Search";
        }

        [DisplayName("Audit Trails")]
        [Description("Audit Trails Permissions")]
        public static class AuditTrails
        {
            public const string View = "Permissions.AuditTrails.View";
            public const string Export = "Permissions.AuditTrails.Export";
            public const string Search = "Permissions.AuditTrails.Search";
        }

        [DisplayName("Licenses")]
        [Description("License Permissions")]
        public static class Licenses
        {
            public const string View = "Permissions.Licenses.View";
            public const string ViewAll = "Permissions.Licenses.ViewAll";
            public const string Export = "Permissions.Licenses.Export";
            public const string Import = "Permissions.Licenses.Import";
            public const string Search = "Permissions.Licenses.Search";
            public const string Create = "Permissions.Licenses.Create";
            public const string Edit = "Permissions.Licenses.Edit";
            public const string Delete = "Permissions.Licenses.Delete";
        }

        [DisplayName("Demo Requests")]
        [Description("Demo Requests Permissions")]
        public static class DemoRequests
        {
            public const string View = "Permissions.DemoRequests.View";
            public const string Search = "Permissions.DemoRequests.Search";
            public const string Edit = "Permissions.DemoRequests.Edit";
            public const string Delete = "Permissions.DemoRequests.Delete";
        }

        [DisplayName("Contact")]
        [Description("Contact Permissions")]
        public static class ContactRequests
        {
            public const string View = "Permissions.ContactRequests.View";
            public const string Search = "Permissions.ContactRequests.Search";
            public const string Edit = "Permissions.ContactRequests.Edit";
            public const string Delete = "Permissions.ContactRequests.Delete";
        }

        [DisplayName("Support Tickets")]
        [Description("Support Ticket Permissions")]
        public static class SupportTickets
        {
            public const string View = "Permissions.SupportTickets.View";
            public const string ViewAll = "Permissions.SupportTickets.ViewAll";
            public const string Search = "Permissions.SupportTickets.Search";
            public const string Edit = "Permissions.SupportTickets.Edit";
            public const string Delete = "Permissions.SupportTickets.Delete";
            public const string ViewAllStatus = "Permissions.SupportTickets.ViewAllStatus";
        }

        [DisplayName("Departments")]
        [Description("Department Permissions")]
        public static class Departments
        {
            public const string View = "Permissions.Departments.View";
            public const string Search = "Permissions.Departments.Search";
            public const string Edit = "Permissions.Departments.Edit";
            public const string Delete = "Permissions.Departments.Delete";
            public const string Add = "Permissions.Departments.Add";
        }

        public static List<string?> GetRegisteredPermissions()
        {
            return (from prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)) select prop.GetValue(null) into propertyValue where propertyValue is not null select propertyValue.ToString()).ToList();
        }
    }
}
