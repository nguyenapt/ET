using System.Threading.Tasks;

namespace ET.DeploymentInformations
{
    public interface IDeploymentInformationAppService 
    {
        object GetLatestVersion();
    }
}


