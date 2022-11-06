using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;
using ET.Resources.Dto;
using ET.SkillLevels.Dto;
using ET.Skills.Dto;

namespace ET.ResourceSkills.Dto
{
    [AutoMapFrom(typeof(ResourceSkill))]
    [AutoMapTo(typeof(ResourceSkill))]
    public class ResourceSkillDto : EntityDto<Guid>
    {
        public Guid ResourceId { get; set; }
        public Guid SkillId { get; set; }
        public Guid SkillLevelId { get; set; }

        public SkillLevelDto SkillLevel { get; set; }
        public SkillDto Skill { get; set; }
        public ResourceDto Resource { get; set; }
    }
}
