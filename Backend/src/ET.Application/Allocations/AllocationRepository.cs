using Abp.Data;
using Abp.EntityFrameworkCore;
using ET.Entities;
using ET.EntityFrameworkCore;
using ET.EntityFrameworkCore.Repositories;
using System;
using System.Collections.Generic;
using ET.Allocations.Dto;
using System.Threading.Tasks;

namespace ET.Allocations
{
    public class AllocationRepository : ETRepositoryBase<Allocation, Guid>
    {
        private readonly IActiveTransactionProvider _transactionProvider;

        public AllocationRepository(IDbContextProvider<ETDbContext> dbContextProvider, 
            IActiveTransactionProvider transactionProvider)
            : base(dbContextProvider, transactionProvider)
        {
            _transactionProvider = transactionProvider;
        }
        public async Task<List<SOWAllocationStatusDto>> GetSOWAllocationStatus(SOWAllocationStatusRequestDto request)
        {
            return await GetResultsByStoreProcedure<SOWAllocationStatusDto, SOWAllocationStatusRequestDto>("dbo.spGetSOWAllocatedResource", request);
        }
        public async Task<List<SOWRolesAllocationStatusDto>> GetSOWRolesAllocationStatus(SOWRolesAllocationStatusRequestDto request)
        {
            return await GetResultsByStoreProcedure<SOWRolesAllocationStatusDto, SOWRolesAllocationStatusRequestDto>("dbo.spGetSOWRolesAllocationStatus", request);
        }
        public async Task<List<AvailableResourceDto>> GetAvailableResources(AllocationResultRequestDto request)
        {
            return await GetResultsByStoreProcedure<AvailableResourceDto, AllocationResultRequestDto>("dbo.spGetAvailableResource", request);
        }
        public async Task<List<AllocationForResourceDto>> GetAllocationDetailForResource(AllocationForResourceRequestDto request)
        {
            return await GetResultsByStoreProcedure<AllocationForResourceDto, AllocationForResourceRequestDto>("dbo.spGetAllocationForResource", request);
        }
        public async Task<List<SowRoleAllocationDto>> GetAllocationsForSOWRole(Guid sowRoleId)
        {
            return await GetResultsBySqlCommand<SowRoleAllocationDto>($"select a.*, c.FirstName, c.LastName, c.EmployeeCode,t.Name AllocationType,s.Name AllocationStatus from dbo.Allocation a join dbo.Resource c on a.ResourceId = c.Id left join dbo.AllocationType t on a.AllocationTypeId=t.Id left join dbo.AllocationStatus s on a.AllocationStatusId=s.Id where a.SOWRoleId = '{sowRoleId}' and a.IsDeleted = 0");
        }

        public async Task<List<AvailableAllocationForUserResponseDto>> GetAvailableAllocationsForUser(AvailableAllocationForUserRequestDto userRequest)
        {
            if (string.IsNullOrWhiteSpace(userRequest?.UserName))
            {
                throw new ArgumentNullException(nameof(AvailableAllocationForUserRequestDto.UserName));
            }

            if (userRequest.UserId == null)
            {
                throw new ArgumentNullException(nameof(AvailableAllocationForUserRequestDto.UserId));
            }

            return await GetResultsByStoreProcedure<AvailableAllocationForUserResponseDto, AvailableAllocationForUserRequestDto>("dbo.spGetAvailableAllocationsForUser", userRequest);
        }
    }
}
