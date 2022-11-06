using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using ET.Entities;
using ET.Resources.Dto;

namespace ET.Resources
{
    public interface IResourceAppService : IAsyncCrudAppService<ResourceDto, Guid, ResourceResultRequestDto, CreateResourceDto, ResourceDto>
    {
        Task<Resource> GetResourceByUserIdAsync(long userId);
    }
}


