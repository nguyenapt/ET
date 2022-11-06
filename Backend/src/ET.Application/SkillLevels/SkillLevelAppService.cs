using System;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using ET.Entities;
using ET.SkillLevels.Dto;

namespace ET.SkillLevels
{
    public class SkillLevelAppService : AsyncCrudAppService<SkillLevel, SkillLevelDto, Guid, SkillLevelResultRequestDto, CreateSkillLevelDto, SkillLevelDto>
    {
        public SkillLevelAppService(IRepository<SkillLevel, Guid> repository) : base(repository)
        {
        }
    }
}
