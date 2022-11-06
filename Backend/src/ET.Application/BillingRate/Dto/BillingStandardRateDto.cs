using System;
using Abp.AutoMapper;

namespace ET.BillingRate.Dto
{
    [AutoMapFrom(typeof(BillingRateResultRequestDto))]
    public class BillingStandardRateDto
    {
        public string BillType { get; set; }
        public string ResourceRole { get; set; }
        public string RateType { get; set; }
        public string Currency { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string Value { get; set; }

    }
}
