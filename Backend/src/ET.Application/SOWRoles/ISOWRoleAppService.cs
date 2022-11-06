using System;
using Abp.Application.Services;
using ET.SOWRoles.Dto;
using System.Threading.Tasks;

namespace ET.SOWRoles
{
    public interface ISOWRoleAppService : IAsyncCrudAppService<SOWRoleDto, Guid, SOWRoleResultRequestDto, CreateSOWRoleDto, UpdateSowRoleDto>
    {
        Task<SoWRoleDetailListDto> GetSoWRoleDetailList(Guid sowId);
    }
}


