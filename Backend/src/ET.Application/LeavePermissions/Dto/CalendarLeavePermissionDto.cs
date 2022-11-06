using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;
using ET.TimesheetEntries.Dto;

namespace ET.LeavePermissions.Dto
{
    public class CalendarLeavePermissionDto 
    {
        public Guid ResourceId { get; set; }
        public string ResourceName { get; set; }        

        public virtual IEnumerable<CalendarItemLeavePermissionDto> CalendarItemLeavePermissions { get; set; }
    }

    public class CalendarItemLeavePermissionDto
    {
        public DateTime Date { get; set; }

        public decimal? Hours { get; set; }

        public string ApproveStatus { get; set; }

        public Guid LeavePermissionId { get; set; }
    }
}
