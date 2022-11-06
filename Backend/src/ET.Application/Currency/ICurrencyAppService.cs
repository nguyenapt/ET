using Abp.Application.Services;
using ET.Currency.Dto;

namespace ET.Currency
{
    public interface ICurrencyAppService : IAsyncCrudAppService<CurrencyDto, int, CurrencyResultRequestDto, CreateCurrencyDto, CurrencyDto>
    {

    }
}
