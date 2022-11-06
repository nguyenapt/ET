using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ET.Roles.Dto;
using ET.Sessions.Dto;
using ET.Users.Dto;

namespace ET.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);        
    }
}
