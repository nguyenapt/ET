using Abp.Application.Services;
using ET.ProjectStateType.Dto;

namespace ET.ProjectStateType
{
    public interface IProjectStateAppService : IAsyncCrudAppService<ProjectStateTypeDto, int, ProjectStateTypeResultRequestDto, CreateProjectStateTypeDto, ProjectStateTypeDto>
    {
    }
}
