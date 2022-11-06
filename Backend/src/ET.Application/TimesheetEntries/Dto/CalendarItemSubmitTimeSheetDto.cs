using System;

namespace ET.TimesheetEntries.Dto
{
    public class CalendarItemSubmitTimeSheetDto
    {
        public DateTime Date { get; set; }
        public decimal? Hours { get; set; }
        public string ApproveStatus { get; set; }
    }
}