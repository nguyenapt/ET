using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using ET.Allocations.Dto;
using ET.Entities;
using ET.AllocationTypes.Dto;

namespace ET.Allocations
{
    public interface IAllocationsAppService : IAsyncCrudAppService<AllocationDto, Guid, AllocationResultRequestDto, CreateAllocationDto, UpdateAllocationDto>
    {
        List<AllocationStatus> GetAllAllocationStatus();
        IEnumerable<AllocationTypeDto> GetAvailableAllocationTypeForSupporters();
    }
}


