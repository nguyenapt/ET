using Abp.Application.Services;
using Abp.Domain.Repositories;
using ET.ProjectStateType.Dto;

namespace ET.ProjectStateType
{
    public class ProjectStateAppService : AsyncCrudAppService<Entities.ProjectStateType, ProjectStateTypeDto, int, ProjectStateTypeResultRequestDto, CreateProjectStateTypeDto, ProjectStateTypeDto>, IProjectStateAppService
    {
        public ProjectStateAppService(IRepository<Entities.ProjectStateType, int> repository) : base(repository)
        {
        }
    }
}
