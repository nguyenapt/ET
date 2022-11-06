using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.LeavePermissions.Dto
{
    [AutoMapTo(typeof(LeavePermission))]
    [AutoMapFrom(typeof(LeavePermission))]
    public class CreateLeavePermissionDto
    {      
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }        

        [Required]
        public Guid LeaveTypeId { get; set; }

        [Required]
        public bool IsFullDay { get; set; }

        public Guid? ApprovalId { get; set; }

        [Required]
        public string Reason { get; set; }

        public List<Guid> NotificationIds { get; set; }
    }
}
