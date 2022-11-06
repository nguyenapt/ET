using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;
using ET.ResourceSkills.Dto;
using ET.Users.Dto;

namespace ET.Resources.Dto
{
    [AutoMapFrom(typeof(Resource))]
    [AutoMapTo(typeof(Resource))]
    public class ResourceDto : EntityDto<Guid>
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
  
        public string LastName { get; set; }
        public string EmployeeCode { get; set; }
        public string Country { get; set; }

        public Guid? DepartmentId { get; set; }

        public Guid? WorkingHourRuleId { get; set; }

        public bool IsKAM { get; set; }
        public DateTime TimeStamp { get; set; }

        public UserDto User { get; set; }
        public List<ResourceSkillDto> ResourceSkills { get; set; }

        public string FullName => FirstName + " " + LastName;

        public string JobTitle { get; set; }
 
        public string JobTitleLevel { get; set; }
    }
}
