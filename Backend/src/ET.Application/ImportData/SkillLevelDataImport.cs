using System;
using Abp.Domain.Repositories;
using ET.Entities;
using ET.ImportData.Dto;

namespace ET.ImportData
{
    public class SkillLevelDataImport : ImportDataBaseAppService<SkillLevel, Guid, SkillLevelImportDto>
    {
        public SkillLevelDataImport(IRepository<SkillLevel, Guid> repository) : base(repository)
        {
        }
    }
}
