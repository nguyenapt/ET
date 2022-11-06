using Abp.AutoMapper;
using ET.Entities;
using Npoi.Mapper.Attributes;

namespace ET.ImportData.Dto
{
    [AutoMapTo(typeof(Beneficiary))]
    public class BeneficialInformationDto
    {
        [Column("Name")]
        public string Name { get; set; }

        [Column("Detail")]
        public string Detail { get; set; }
    }
}