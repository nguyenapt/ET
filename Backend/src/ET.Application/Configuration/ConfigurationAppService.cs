using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ET.Configuration.Dto;

namespace ET.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ETAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
