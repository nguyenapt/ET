using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ET.MultiTenancy.Dto;

namespace ET.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

