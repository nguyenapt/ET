using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.LeaveTypes.Dto;
using ET.Resources.Dto;
using ET.Entities;

namespace ET.LeaveBanks.Dto
{
    [AutoMapFrom(typeof(LeaveBank))]
    [AutoMapTo(typeof(LeaveBank))]
    public class LeaveBankDto : EntityDto<Guid>
    {
        public Guid ResourceId { get; set; }
        public decimal TotalAllowedHours { get; set; }
        public int Year { get; set; }
        
        public Guid LeaveTypeId { get; set; }
        public ResourceDto Resource { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
    }
}
