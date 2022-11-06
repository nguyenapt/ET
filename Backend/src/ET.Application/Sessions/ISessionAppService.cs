using System.Threading.Tasks;
using Abp.Application.Services;
using ET.Sessions.Dto;

namespace ET.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
