using System;
using System.Collections.Generic;
using ET.Tasks.Dto;

namespace ET.TimesheetEntries.Dto
{
    public class CreateOrUpdateTimeSheetEntryRequestDto
    {
        public Guid? AllocationId { get; set; }
        public TaskDto TaskInFormation { get; set; }
        public string Description { get; set; }
        public Guid ApprovalId { get; set; }
        public string TicketName { get; set; }
        public Guid? LeavePermissionId { get; set; }
        public Guid? LeaveTypeId { get; set; }
        public bool IsLeaveType { get; set; }
        public List<TimeSheetEntryRequestInDay> TimeSheetEntriesInDay { get; set; }
    }

    public class TimeSheetEntryRequestInDay
    {
        public string ApprovalStatus { get; set; }
        public Guid? TimeSheetEntryId { get; set; }
        public DateTime RecordDate { get; set; }
        public decimal Hours { get; set; }
        public string Day => RecordDate.ToString("dddd");
        public bool? IsOverTime { get; set; }
    }
}
