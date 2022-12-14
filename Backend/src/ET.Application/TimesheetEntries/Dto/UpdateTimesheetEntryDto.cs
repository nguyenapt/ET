using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace ET.TimesheetEntries
{
    [AutoMapFrom(typeof(TimesheetEntry))]
    [AutoMapTo(typeof(TimesheetEntry))]
    public class UpdateTimesheetEntryDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public Guid? TaskId { get; set; }

        [Required]
        public Guid? AllocationId { get; set; }
        public Guid? LeavePermissionId { get; set; }
        public string Description { get; set; }
        public string TicketName { get; set; }
        public decimal Hours { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }
        public DateTime? SubmittedTimestamp { get; set; }
        public DateTime? ApprovedTimestamp { get; set; }
        public bool? IsOverTime { get; set; }
        public Guid? CustomBilledProjectId { get; set; }
        public bool? ApproveStatus { get; set; }
        public Guid? ApprovalId { get; set; }
    }
}

