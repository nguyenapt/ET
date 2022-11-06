using System;
using Abp.Domain.Repositories;
using ET.Entities;
using ET.ImportData.Dto;

namespace ET.ImportData
{
    public class DepartmentDataImport : ImportDataBaseAppService<Department, Guid, DepartmentImportDto>
    {
        public DepartmentDataImport(IRepository<Department, Guid> repository) : base(repository)
        {
        }
    }
}
