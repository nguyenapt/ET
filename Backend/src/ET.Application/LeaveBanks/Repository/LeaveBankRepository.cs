using Abp.Data;
using Abp.EntityFrameworkCore;
using ET.Entities;
using ET.EntityFrameworkCore;
using ET.EntityFrameworkCore.Repositories;
using System;

namespace ET.LeaveBanks.Repository
{
    public class LeaveBankRepository : ETRepositoryBase<LeaveBank, Guid>
    {        
        public LeaveBankRepository(IDbContextProvider<ETDbContext> dbContextProvider,
            IActiveTransactionProvider transactionProvider)
            : base(dbContextProvider, transactionProvider)
        {
            
        }
    }
}
