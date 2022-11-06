using Abp.AutoMapper;
using ET.Entities;

namespace ET.ImportData.Dto
{
    [AutoMapTo(typeof(SkillLevel))]
    public class SkillLevelImportDto
    {
        public string Level { get; set; }
        public string Description { get; set; }
    }
}
