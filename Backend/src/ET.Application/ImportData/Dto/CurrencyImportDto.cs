using Abp.AutoMapper;

namespace ET.ImportData.Dto
{
    [AutoMapTo(typeof(Entities.Currency))]
    public class CurrencyImportDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
