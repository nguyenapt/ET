using Abp.Data;
using Abp.EntityFrameworkCore;
using ET.Entities;
using ET.EntityFrameworkCore;
using ET.EntityFrameworkCore.Repositories;
using ET.InternalTypes.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ET.InternalTypes
{
    public class InternalTypeRepository : ETRepositoryBase<InternalType, Guid>
    {
        private readonly IActiveTransactionProvider _transactionProvider;
        private readonly string InternalType = "Internal";

        public InternalTypeRepository(IDbContextProvider<ETDbContext> dbContextProvider,
            IActiveTransactionProvider transactionProvider)
            : base(dbContextProvider, transactionProvider)
        {
            _transactionProvider = transactionProvider;
        }

        public async Task<bool> IsInternalTypeSupporter(IsInternalTypeSupporterRequestDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(IsInternalTypeSupporterRequestDto));
            }

            if (!request.InternalTypeId.HasValue)
            {
                return false;
            }

            var result = await base.GetAsync(request.InternalTypeId.Value);
            if (result == null)
            {
                return false;
            }

            if (result.Name.Equals(InternalType, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public async Task<List<InternalTypeDto>> GetInternalTypeSupporter()
        {
            var result = await GetResultsBySqlCommand<InternalTypeDto>($"SELECT TOP 1 * FROM dbo.InternalType where Name = '{InternalType}'");
            return result;
        }
    }
}
