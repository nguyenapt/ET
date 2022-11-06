using System;

namespace ET.TimesheetEntries.Dto
{
    public class GetTimeSheetsForUserRequestDto
    {
        public Guid ResourceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
