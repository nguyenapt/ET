using System.Threading.Tasks;
using ET.Configuration.Dto;

namespace ET.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
