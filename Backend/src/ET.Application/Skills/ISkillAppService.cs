using System;
using Abp.Application.Services;
using ET.Skills.Dto;

namespace ET.Skills
{
    public interface ISkillAppService : IAsyncCrudAppService<SkillDto, Guid, SkillResultRequestDto, CreateSkillDto, SkillDto>
    {
    }
}


