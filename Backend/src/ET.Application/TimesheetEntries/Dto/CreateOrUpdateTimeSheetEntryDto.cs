using System;
using System.ComponentModel.DataAnnotations;

namespace ET.TimesheetEntries.Dto
{
    public class CreateOrUpdateTimeSheetEntryDto
    {
        public Guid? TimeSheetEntryId { get; set; }

        public Guid? TaskId { get; set; }

        [Required]
        public Guid? AllocationId { get; set; }

        public Guid? LeavePermissionId { get; set; }
        
        public string Description { get; set; }

        public string TicketName { get; set; }

        [Required]
        public decimal Hours { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }
        
        public bool? IsOverTime { get; set; }
       
        public Guid? ApprovalId { get; set; }

        public bool IsLeaveType { get; set; }
        public Guid? LeaveTypeId { get; set; }
    }
}
