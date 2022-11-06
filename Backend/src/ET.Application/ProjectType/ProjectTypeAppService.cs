using Abp.Application.Services;
using Abp.Domain.Repositories;
using ET.ProjectType.Dto;

namespace ET.ProjectType
{
    public class ProjectTypeAppService : AsyncCrudAppService<Entities.ProjectType, ProjectTypeDto, int, ProjectTypeResultRequestDto, CreateProjectTypeDto, CreateProjectTypeDto>, IProjectTypeAppService
    {
        public ProjectTypeAppService(IRepository<Entities.ProjectType, int> repository) : base(repository)
        {
        }
    }
}
