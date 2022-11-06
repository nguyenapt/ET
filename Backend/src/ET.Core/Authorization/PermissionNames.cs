namespace ET.Authorization
{
    public static class PermissionNames
    {
        #region Admin permissions
        public const string Pages_Systems = "Pages.Systems";
        public const string Pages_Tenants = "Pages.Tenants";

        public const string Pages_Users = "Pages.Users";

        public const string Pages_Roles = "Pages.Roles";

        public const string Pages_SiteSettings = "Pages.SiteSettings";

        #endregion // Admin permissions

        #region SoWs
        public const string Pages_SoWs = "Pages.SoWs";
        public const string Pages_SoWs_CRUD = "Pages.SoWs.CRUD";
        public const string Pages_SoWs_Approval = "Pages.SoWs.Approval";
        #endregion // End SoWs

        #region ProjectRoles
        public const string Pages_ProjectRoles = "Pages.ProjectRoles";
        public const string Pages_ProjectRoles_CRUD = "Pages.ProjectRoles.CRUD";
        #endregion // ProjectRoles

        #region Resources
        public const string Pages_Resources = "Pages.Resources";
        public const string Pages_Resources_CRUD = "Pages.Resources.CRUD";
        #endregion // Resources

        #region Allocation Planner
        public const string Pages_Allocations = "Pages.Allocations";
        #endregion // Resources

        #region Projects
        public const string Pages_Projects = "Pages.Projects";
        public const string Pages_Projects_CRUD = "Pages.Projects.CRUD";
        #endregion // Projects

        #region Clients
        public const string Pages_Clients = "Pages.Clients";
        public const string Pages_Clients_CRUD = "Pages.Clients.CRUD";
        #endregion // Clients

        #region TimeSheet/Leaves
        public const string Pages_MyTimeSheet = "Pages.MyTimeSheet";
        public const string Pages_TimesheetApproval = "Pages.TimesheetApproval";
        public const string Pages_MyLeaves = "Pages.MyLeaves";
        public const string Pages_LeaveRequestsApproval = "Pages.Pages_LeaveRequestsApproval";
        #endregion

    }
}
