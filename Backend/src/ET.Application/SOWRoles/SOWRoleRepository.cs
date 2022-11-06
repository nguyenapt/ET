using Abp.Data;
using Abp.EntityFrameworkCore;
using ET.Entities;
using ET.EntityFrameworkCore;
using ET.EntityFrameworkCore.Repositories;
using ET.SOWRoles.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ET.SOWRoles
{
    public class SOWRoleRepository : ETRepositoryBase<SOWRole, Guid>
    {
        private readonly IActiveTransactionProvider _transactionProvider;

        public SOWRoleRepository(IDbContextProvider<ETDbContext> dbContextProvider,
            IActiveTransactionProvider transactionProvider)
            : base(dbContextProvider, transactionProvider)
        {
            _transactionProvider = transactionProvider;
        }

        public async Task<List<SOWRoleDto>> GetSowRolesWithSupporterType(SowRolesRequestDto request)
        {
            var result = await GetResultsByStoreProcedure<SOWRoleDto, SowRolesRequestDto>("dbo.spGetSowRolesWithSupporterType", request);
            return result;
        }

        public async Task<List<SOWRole>> GetNonSupporterSowRoles(SowRolesRequestDto request)
        {
            if (request == null || request?.SowId == null)
            {
                throw new ArgumentNullException(nameof(SowRolesRequestDto));
            }

            var result = await GetResultsByStoreProcedure<SOWRole, SowRolesRequestDto>("dbo.spGetNonSupporterSOWRoles", request);
            return result;
        }
    }
}
