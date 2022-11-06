using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using ET.BillingRate.Dto;

namespace ET.BillingRate
{
    public class BillingRateAppService : AsyncCrudAppService<Entities.BillingRate, BillingRateDto, int, BillingRateResultRequestDto, CreateBillingRateDto, BillingRateDto>, IBillingRateAppService
    {
        public BillingRateAppService(IRepository<Entities.BillingRate, int> repository) : base(repository)
        {
        }

        public BillingStandardRateDto GetStandardRate(BillingRateResultRequestDto input)
        {
            var billingRate = GetAllAsync(input).Result.Items.FirstOrDefault();

            var standardRateDto = ObjectMapper.Map<BillingStandardRateDto>(input);
            standardRateDto.Value = GetStandardRate(input.RateType, billingRate);
            return standardRateDto;
        }

        private string GetStandardRate(string rateType, BillingRateDto billingRate)
        {
            if (billingRate != null && Enum.TryParse(rateType, out AppEnums.RateType eRateType))
            {
                return eRateType switch
                {
                    AppEnums.RateType.Monthly => billingRate.MonthlyRate,
                    AppEnums.RateType.Daily => billingRate.DailyRate,
                    AppEnums.RateType.Hourly => billingRate.HourlyRate,
                    _ => string.Empty
                };
            }

            return string.Empty;
        }

        protected override IQueryable<Entities.BillingRate> CreateFilteredQuery(BillingRateResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                .Where(x => x.ResourceRole == input.ResourceRole)
                .Where(x => x.Currency == input.Currency)
                .WhereIf(!input.EffectiveDate.HasValue, x => x.EffectiveDate <= DateTime.UtcNow)
                .WhereIf(input.EffectiveDate.HasValue, x => x.EffectiveDate <= input.EffectiveDate)
                .OrderByDescending(x => x.EffectiveDate);
        }
    }
}
