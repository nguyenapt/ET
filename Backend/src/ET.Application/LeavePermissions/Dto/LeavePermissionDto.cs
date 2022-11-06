using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;
using ET.LeaveTypes.Dto;
using ET.Resources.Dto;
using ET.TimesheetEntries.Dto;

namespace ET.LeavePermissions.Dto
{
    [AutoMapFrom(typeof(LeavePermission))]
    [AutoMapTo(typeof(LeavePermission))]
    public class LeavePermissionDto : EntityDto<Guid>
    {
        public Guid ResourceId { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalHours { get; set; }
        public Guid LeaveTypeId { get; set; }

        public bool IsFullDay { get; set; }

        public Guid? ApprovalId { get; set; }

        public byte? ApprovalStatus { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public DateTime CreationTime { get; set; }

        public ResourceDto Resource { get; set; }

        public ResourceDto ApprovalUser { get; set; }

        public LeaveTypeDto LeaveType { get; set; }
      
        public List<TimesheetEntryDto> TimesheetEntries { get; set; }

        public string Reason { get; set; }

        public string RejectReason { get; set; }
    }
}
