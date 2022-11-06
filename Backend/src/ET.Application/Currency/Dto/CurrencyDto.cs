using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace ET.Currency.Dto
{
    [AutoMapTo(typeof(Entities.Currency))]
    [AutoMapFrom(typeof(Entities.Currency))]

    public class CurrencyDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
