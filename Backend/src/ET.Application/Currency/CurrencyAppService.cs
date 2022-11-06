using Abp.Application.Services;
using Abp.Domain.Repositories;
using ET.Currency.Dto;

namespace ET.Currency
{
    public class CurrencyAppService : AsyncCrudAppService<Entities.Currency, CurrencyDto, int, CurrencyResultRequestDto, CreateCurrencyDto, CurrencyDto>, ICurrencyAppService
    {
        public CurrencyAppService(IRepository<Entities.Currency, int> repository) : base(repository)
        {
        }
    }
}
