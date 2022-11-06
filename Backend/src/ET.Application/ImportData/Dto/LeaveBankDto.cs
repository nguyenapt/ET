using Abp.AutoMapper;
using ET.Entities;
using Npoi.Mapper.Attributes;

namespace ET.ImportData.Dto
{    
    public class LeaveBankDto
    {
        [Column("Resource Email")]
        public string ResourceEmail	 { get; set; }

        [Column("Leave Type")]
        public string LeaveTypeName { get; set; }

        [Column("FY")]
        public int Year { get; set; }

        [Column("Hour")]
        public decimal TotalAllowedHours { get; set; }
    }
}
