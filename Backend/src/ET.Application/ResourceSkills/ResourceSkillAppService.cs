using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ET.ResourceSkills.Dto;
using ET.Entities;
using System.Collections.Generic;

namespace ET.ResourceSkills
{
    public class ResourceSkillAppService : AsyncCrudAppService<ResourceSkill, ResourceSkillDto, Guid, ResourceSkillResultRequestDto, CreateResourceSkillDto, ResourceSkillDto>, IResourceSkillAppService
    {
        public ResourceSkillAppService(IRepository<ResourceSkill, Guid> repository) : base(repository)
        {
        }
        public async Task<ListResultDto<ResourceSkillDto>> CreateOrUpdateAsync(List<ResourceSkillDto> SkillList)
        {
            var resultItems = new List<ResourceSkillDto>();
            if(SkillList==null || !SkillList.Any())
            {
                return new ListResultDto<ResourceSkillDto>();
            }
            //add/update skill list
            foreach(var item in SkillList)
            {
                var resourceSkill = ObjectMapper.Map<ResourceSkill>(item);
                var resultItem = await Repository.InsertOrUpdateAsync(resourceSkill);
                resultItems.Add(ObjectMapper.Map<ResourceSkillDto>(resultItem));
            }
            return new ListResultDto<ResourceSkillDto>(ObjectMapper.Map<List<ResourceSkillDto>>(resultItems));
        }
    }
}

