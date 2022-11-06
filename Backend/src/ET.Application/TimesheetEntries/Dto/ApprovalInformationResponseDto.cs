using System;

namespace ET.TimesheetEntries.Dto
{
    public class ApprovalInformationResponseDto
    {
        public Guid TimeSheetEntryId { get; set; }
        public string SubmitterFullName { get; set; }
        public DateTime RecordDate { get; set; }
        public string ApprovalEmail { get; set; }
        public string ApprovalFullName { get; set; }
        public Guid SubmitterResourceId { get; set; }
    }
}
