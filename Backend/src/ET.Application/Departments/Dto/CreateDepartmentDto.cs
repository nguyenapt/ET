using Abp.AutoMapper;
using ET.Entities;

namespace ET.Departments.Dto
{
    [AutoMapTo(typeof(Department))]
    public class CreateDepartmentDto
    {
        public string Name { get; set; }
        public string DepartmentCode { get; set; }
    }
}
