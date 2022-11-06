using System;

namespace ET.TimesheetEntries.Dto
{
    public class SubmitterTimeSheetInformationDto
    {
        public string SubmitterEmail { get; set; }
        public string SubmitterFullName { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
