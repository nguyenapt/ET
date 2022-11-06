using System;
using System.Collections.Generic;

namespace ET.TimesheetEntries.Dto
{
    public class TimeSheetSubmitterCalendarDto
    {
        public Guid ResourceId { get; set; }
        public string ResourceName { get; set; }
        public decimal? TotalHours { get; set; }
        public virtual IEnumerable<CalendarItemSubmitTimeSheetDto> CalendarItemSubmitTimeSheets { get; set; }
    }
}
