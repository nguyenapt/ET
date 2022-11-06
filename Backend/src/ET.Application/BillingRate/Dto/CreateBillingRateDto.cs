using Abp.AutoMapper;

namespace ET.BillingRate.Dto
{
    [AutoMapTo(typeof(Entities.BillingRate))]

    public class CreateBillingRateDto
    {
    }
}
