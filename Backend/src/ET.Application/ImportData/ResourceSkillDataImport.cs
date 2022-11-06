using System;
using Abp.Domain.Repositories;
using ET.Entities;
using ET.ImportData.Dto;

namespace ET.ImportData
{
    public class ResourceSkillDataImport : ImportDataBaseAppService<Skill, Guid, ResourceSkillDto>
    {
        public ResourceSkillDataImport(IRepository<Skill, Guid> repository) : base(repository)
        {
        }
    }
}
