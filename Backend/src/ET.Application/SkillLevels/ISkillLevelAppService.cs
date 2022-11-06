using System;
using Abp.Application.Services;
using ET.SkillLevels.Dto;

namespace ET.SkillLevels
{
    public interface ISkillLevelAppService : IAsyncCrudAppService<SkillLevelDto, Guid, SkillLevelResultRequestDto, CreateSkillLevelDto, SkillLevelDto>
    {
    }
}
