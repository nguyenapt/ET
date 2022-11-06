using System;
using Abp.Application.Services;
using ET.Departments.Dto;
using ET.SoW.Dto;

namespace ET.Departments
{
    public interface IDepartmentAppService : IAsyncCrudAppService<DepartmentDto, Guid, DepartmentResultRequestDto, CreateDepartmentDto, DepartmentDto>
    {
    }
}


