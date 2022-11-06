using Abp.Authorization;
using ET.Authorization.Roles;
using ET.Authorization.Users;

namespace ET.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
