using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ET.Controllers
{
    public abstract class ETControllerBase: AbpController
    {
        protected ETControllerBase()
        {
            LocalizationSourceName = ETConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
