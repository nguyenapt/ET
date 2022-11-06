using System.Threading.Tasks;
using Abp.Application.Services;
using ET.Authorization.Accounts.Dto;

namespace ET.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<RegisterOutput> Register(RegisterInput input);
    }
}
