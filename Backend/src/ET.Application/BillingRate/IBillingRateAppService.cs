using Abp.Application.Services;
using ET.BillingRate.Dto;

namespace ET.BillingRate
{
    public interface IBillingRateAppService : IAsyncCrudAppService<BillingRateDto, int, BillingRateResultRequestDto, CreateBillingRateDto, BillingRateDto>
    {
        public BillingStandardRateDto GetStandardRate(BillingRateResultRequestDto input);
    }
}
