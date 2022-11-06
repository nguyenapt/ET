using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.Departments.Dto
{
    [AutoMapFrom(typeof(Department))]
    [AutoMapTo(typeof(Department))]
    public class DepartmentDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string DepartmentCode { get; set; }
    }
}
