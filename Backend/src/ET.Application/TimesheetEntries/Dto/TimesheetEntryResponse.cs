using System;
using System.Collections.Generic;
using ET.TaskCategorys.Dto;
using ET.Tasks.Dto;

namespace ET.TimesheetEntries.Dto
{
    public class TimeSheetEntryResponse
    {
        public Guid? AllocationId { get; set; }
        public string SowCode { get; set; }
        public TaskDto TaskInformation { get; set; }
        public TaskCategoryDto TaskCategory { get; set; }
        public string Description { get; set; }
        public Guid? ApprovalId { get; set; }
        public string TicketName { get; set; }
        public Guid? LeavePermissionId { get; set; }
        public Guid? LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public bool IsLeaveType => LeavePermissionId.HasValue;

        public List<TimeSheetEntryInDay> TimeSheetEntriesInDay { get; set; }
    }

    public class TimeSheetEntryInDay
    {
        public Guid? TimeSheetEntryId { get; set; }
        public DateTime RecordDate { get; set; }
        public decimal Hours { get; set; }
        public string ApprovalStatus { get; set; }
        public string Day => RecordDate.ToString("dddd");
        public bool? IsOverTime { get; set; }
    }
}