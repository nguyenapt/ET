using Abp.AutoMapper;
using Npoi.Mapper.Attributes;

namespace ET.ImportData.Dto
{
    [AutoMapTo(typeof(Entities.ProjectType))]
    public class ProjectTypeImportDto
    {
        public string Name { get; set; }
        public string Scope { get; set; }
        [Column("P/L")]
        public string PL { get; set; }
    }
}
