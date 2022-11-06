using Abp.Domain.Repositories;
using ET.ImportData.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ET.ImportData
{
    public class ResourceRoleDataImport : ImportDataBaseAppService<Entities.ResourceRole, int, ResourceRoleDto>
    {
        public ResourceRoleDataImport(IRepository<Entities.ResourceRole, int> repository) : base(repository)
        {
        }
    }
}
