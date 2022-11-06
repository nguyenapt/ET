using System;
using Abp.Application.Services;
using ET.AllocationTypes.Dto;

namespace ET.AllocationTypes
{
    public interface IAllocationTypeAppService : IAsyncCrudAppService<AllocationTypeDto, int, AllocationTypeResultRequestDto, CreateAllocationTypeDto, AllocationTypeDto>
    {
    }
}


