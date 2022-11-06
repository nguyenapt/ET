using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace ET.BillingRate.Dto
{
    [AutoMapFrom(typeof(Entities.BillingRate))]
    [AutoMapTo(typeof(Entities.BillingRate))]
    public class BillingRateDto : EntityDto<int>
    {
        public string ResourceRole { get; set; }
        public string Currency { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string MonthlyRate { get; set; }
        public string DailyRate { get; set; }
        public string HourlyRate { get; set; }
    }
}
