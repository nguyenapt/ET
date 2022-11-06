using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.LeaveTypes.Dto
{
    [AutoMapTo(typeof(LeaveType))]
    public class CreateLeaveTypeDto
    {
        public string Name { get; set; }
    }
}
