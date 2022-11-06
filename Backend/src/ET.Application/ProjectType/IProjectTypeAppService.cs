using Abp.Application.Services;
using ET.ProjectType.Dto;

namespace ET.ProjectType
{
    public interface IProjectTypeAppService : IAsyncCrudAppService<ProjectTypeDto, int, ProjectTypeResultRequestDto, CreateProjectTypeDto, CreateProjectTypeDto>
    {
    }
}
