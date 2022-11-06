using System;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using ET.AllocationTypes.Dto;
using ET.Entities;

namespace ET.AllocationTypes
{
    public class AllocationTypeAppService : AsyncCrudAppService<AllocationType, AllocationTypeDto, int, AllocationTypeResultRequestDto, CreateAllocationTypeDto, AllocationTypeDto>, IAllocationTypeAppService
    {
        public AllocationTypeAppService(IRepository<AllocationType, int> repository) : base(repository)
        {
        }
    }
}

