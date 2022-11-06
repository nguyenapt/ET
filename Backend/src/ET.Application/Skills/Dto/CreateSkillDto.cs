using Abp.AutoMapper;
using ET.Entities;

namespace ET.Skills.Dto
{
    [AutoMapTo(typeof(Skill))]
    public class CreateSkillDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
