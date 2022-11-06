using Abp.AutoMapper;
using ET.Entities;

namespace ET.ImportData.Dto
{
    [AutoMapTo(typeof(Skill))]
    public class ResourceSkillDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
