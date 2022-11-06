using System;
using Abp.Application.Services;
using ET.ResourceSkills.Dto;
using ET.SoW.Dto;

namespace ET.ResourceSkills
{
    public interface IResourceSkillAppService : IAsyncCrudAppService<ResourceSkillDto, Guid, ResourceSkillResultRequestDto, CreateResourceSkillDto, ResourceSkillDto>
    {
    }
}


