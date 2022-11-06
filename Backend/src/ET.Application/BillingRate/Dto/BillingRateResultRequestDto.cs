using System;

namespace ET.BillingRate.Dto
{
    public class BillingRateResultRequestDto
    {
        public string ResourceRole { get; set; }
        public string RateType { get; set; }
        public string Currency { get; set; }
        public DateTime? EffectiveDate { get; set; }
    }
}
