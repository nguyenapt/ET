using Abp.Domain.Repositories;
using ET.ImportData.Dto;

namespace ET.ImportData
{
    public class BillingRateDataImport : ImportDataBaseAppService<Entities.BillingRate, int, BillingRateDto>
    {
        public BillingRateDataImport(IRepository<Entities.BillingRate, int> repository) : base(repository)
        {
        }
    }
}
