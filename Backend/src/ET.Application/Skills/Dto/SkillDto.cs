using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.Skills.Dto
{
    [AutoMapFrom(typeof(Skill))]
    [AutoMapTo(typeof(Skill))]
    public class SkillDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
