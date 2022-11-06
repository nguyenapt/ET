using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.SkillLevels.Dto
{
    [AutoMapFrom(typeof(SkillLevel))]
    [AutoMapTo(typeof(SkillLevel))]
    public class SkillLevelDto : EntityDto<Guid>
    {
        public string Level { get; set; }
        public string Description { get; set; }
    }
}
