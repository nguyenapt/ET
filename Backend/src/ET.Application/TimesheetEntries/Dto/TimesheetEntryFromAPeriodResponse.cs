using System;

namespace ET.TimesheetEntries.Dto
{
    public class TimeSheetEntryFromAPeriodResponse
    {
        public Guid TimeSheetEntryId { get; set; }
        public Guid? AllocationId { get; set; }
        public string SowCode { get; set; }
        public string TaskCategoryName { get; set; }
        public string TaskName { get; set; }
        public Guid? TaskId { get; set; }
        public Guid? TaskCategoryId { get; set; }
        public string Description { get; set; }
        public decimal Hours { get; set; }
        public DateTime RecordDate { get; set; }
        public bool? IsOverTime { get; set; }
        public byte? ApprovalStatus { get; set; }
        public Guid? ApprovalId { get; set; }
        public string TicketName { get; set; }
        public Guid? LeavePermissionId { get; set; }
        public Guid? LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
    }
}
