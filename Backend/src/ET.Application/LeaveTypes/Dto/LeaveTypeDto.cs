using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.LeaveTypes.Dto
{
    [AutoMapFrom(typeof(LeaveType))]
    [AutoMapTo(typeof(LeaveType))]
    public class LeaveTypeDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public decimal RemainingLeave { get; set; }

        public decimal TotalAllowedLeave { get; set; }
    }
}
