using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.LeaveTypes.Dto;
using ET.Resources.Dto;
using ET.Entities;
using System.ComponentModel.DataAnnotations;

namespace ET.LeaveBanks.Dto
{
    [AutoMapFrom(typeof(LeaveBank))]
    [AutoMapTo(typeof(LeaveBank))]
    public class CreateLeaveBankDto : EntityDto<Guid>
    {
        [Required]
        public Guid ResourceId { get; set; }

        [Required]
        public decimal TotalAllowedHours { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public Guid LeaveTypeId { get; set; }
    }
}
