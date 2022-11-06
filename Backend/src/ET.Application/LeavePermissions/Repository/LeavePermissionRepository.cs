using Abp.Data;
using Abp.EntityFrameworkCore;
using ET.Entities;
using ET.EntityFrameworkCore;
using ET.EntityFrameworkCore.Repositories;
using System;

namespace ET.LeavePermissions.Repository
{
    public class LeavePermissionRepository : ETRepositoryBase<LeavePermission, Guid>
    {        
        public LeavePermissionRepository(IDbContextProvider<ETDbContext> dbContextProvider,
            IActiveTransactionProvider transactionProvider)
            : base(dbContextProvider, transactionProvider)
        {
            
        }
    }
}
