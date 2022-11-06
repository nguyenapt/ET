using Abp.AutoMapper;
using ET.Entities;
using Npoi.Mapper.Attributes;

namespace ET.ImportData.Dto
{
    [AutoMapTo(typeof(Holiday))]
    public class HolidayBankDto
    {
        [Column("Country")]
        public string Country { get; set; }

        [Column("HolidayDate", CustomFormat = "dd-mm-yyyy")]
        public string HolidayDate { get; set; }

        [Column("HolidayName")]
        public string HolidayName { get; set; }
    }
}
