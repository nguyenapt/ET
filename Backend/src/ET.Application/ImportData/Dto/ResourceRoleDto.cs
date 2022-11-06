using Abp.AutoMapper;
using Npoi.Mapper.Attributes;

namespace ET.ImportData.Dto
{
    [AutoMapTo(typeof(Entities.ResourceRole))]
    public class ResourceRoleDto
    {
        [Column("Name")]
        public string Name { get; set; }
        [Column("Value")]
        public string Value { get; set; }
    }
}
