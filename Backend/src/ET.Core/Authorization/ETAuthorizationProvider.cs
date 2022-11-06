using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace ET.Authorization
{
    public class ETAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            #region System permissions
            context.CreatePermission(PermissionNames.Pages_Systems, L(PermissionNames.Pages_Systems));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_SiteSettings, L("SiteSettings"));
            #endregion System permissions

            #region SoWs
            context.CreatePermission(PermissionNames.Pages_SoWs, L(PermissionNames.Pages_SoWs));
            context.CreatePermission(PermissionNames.Pages_SoWs_CRUD, L(PermissionNames.Pages_SoWs_CRUD));
            context.CreatePermission(PermissionNames.Pages_SoWs_Approval, L(PermissionNames.Pages_SoWs_Approval));
            #endregion

            #region Project Roles
            context.CreatePermission(PermissionNames.Pages_ProjectRoles, L(PermissionNames.Pages_ProjectRoles));
            context.CreatePermission(PermissionNames.Pages_ProjectRoles_CRUD, L(PermissionNames.Pages_ProjectRoles_CRUD));
            #endregion // Project Roles

            #region Resources
            context.CreatePermission(PermissionNames.Pages_Resources, L(PermissionNames.Pages_Resources));
            context.CreatePermission(PermissionNames.Pages_Resources_CRUD, L(PermissionNames.Pages_Resources_CRUD));
            #endregion // Resourecs

            #region Allocation Planner
            context.CreatePermission(PermissionNames.Pages_Allocations, L(PermissionNames.Pages_Allocations));
            #endregion // Allocation Planner

            #region Projects
            context.CreatePermission(PermissionNames.Pages_Projects, L(PermissionNames.Pages_Projects));
            context.CreatePermission(PermissionNames.Pages_Projects_CRUD, L(PermissionNames.Pages_Projects_CRUD));
            #endregion // Projects

            #region Clients
            context.CreatePermission(PermissionNames.Pages_Clients, L(PermissionNames.Pages_Clients));
            context.CreatePermission(PermissionNames.Pages_Clients_CRUD, L(PermissionNames.Pages_Clients_CRUD));
            #endregion // Clients

            #region TimeSheet/Leaves
            context.CreatePermission(PermissionNames.Pages_MyTimeSheet, L(PermissionNames.Pages_MyTimeSheet));
            context.CreatePermission(PermissionNames.Pages_TimesheetApproval, L(PermissionNames.Pages_TimesheetApproval));
            context.CreatePermission(PermissionNames.Pages_MyLeaves, L(PermissionNames.Pages_MyLeaves));
            context.CreatePermission(PermissionNames.Pages_LeaveRequestsApproval, L(PermissionNames.Pages_LeaveRequestsApproval));
            #endregion // TimeSheet/Leaves
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ETConsts.LocalizationSourceName);
        }
    }
}
