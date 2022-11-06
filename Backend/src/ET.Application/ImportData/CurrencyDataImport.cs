using Abp.Domain.Repositories;
using ET.ImportData.Dto;

namespace ET.ImportData
{
    public class CurrencyDataImport : ImportDataBaseAppService<Entities.Currency, int, CurrencyImportDto>
    {
        public CurrencyDataImport(IRepository<Entities.Currency, int> repository) : base(repository)
        {
        }
    }
}
