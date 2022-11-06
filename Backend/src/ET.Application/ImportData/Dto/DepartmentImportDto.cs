using Abp.AutoMapper;
using ET.Entities;

namespace ET.ImportData.Dto
{
    [AutoMapTo(typeof(Department))]
    public class DepartmentImportDto
    {
        public string Name { get; set; }
        public string DepartmentCode { get; set; }
    }
}
