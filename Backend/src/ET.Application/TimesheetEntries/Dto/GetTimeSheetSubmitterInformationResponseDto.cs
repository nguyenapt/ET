using System;
using System.Collections.Generic;

namespace ET.TimesheetEntries.Dto
{
    public class GetTimeSheetSubmitterInformationResponseDto
    {
        public Guid ResourceId { get; set; }
        public string ResourceName { get; set; }
        public Guid? TimeSheetEntryId { get; set; }
        public Guid? AllocationId { get; set; }
        public byte? ApprovalStatus { get; set; }
        public decimal? Hours { get; set; }
        public DateTime RecordDate { get; set; }
        public Guid? ApprovalId { get; set; }
    }
}
