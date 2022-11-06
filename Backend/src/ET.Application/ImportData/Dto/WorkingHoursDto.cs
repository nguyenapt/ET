using Abp.AutoMapper;
using ET.Entities;
using Npoi.Mapper.Attributes;

namespace ET.ImportData.Dto
{
    [AutoMapTo(typeof(WorkingHourRule))]
    public class WorkingHoursDto
    {
        [Column("Name")]
        public string Name { get; set; }

        [Column("RequiredMondayHours")]
        public float? RequiredMondayHours { get; set; }

        [Column("RequiredTuesdayHours")]
        public float? RequiredTuesdayHours { get; set; }

        [Column("RequiredWednesdayHours")] 
        public float? RequiredWednesdayHours { get; set; }

        [Column("RequiredThursdayHours")]
        public float? RequiredThursdayHours { get; set; }

        [Column("RequiredFridayHours")]
        public float? RequiredFridayHours { get; set; }

        [Column("RequiredSaturdayHours")]
        public float? RequiredSaturdayHours { get; set; }

        [Column("RequiredSundayHours")]
        public float? RequiredSundayHours { get; set; }
    }
}
