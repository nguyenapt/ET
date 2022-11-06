using System;
using Abp.Application.Services;
using ET.Projects.Dto;

namespace ET.Projects
{
    public interface IProjectAppService : IAsyncCrudAppService<ProjectDto, Guid, ProjectResultRequestDto, CreateProjectDto, UpdateProjectDto>
    {
    }
}


