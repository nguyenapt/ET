using System;
using Abp.Application.Services;
using ET.Tasks.Dto;
using ET.SoW.Dto;

namespace ET.Tasks
{
    public interface ITaskAppService : IAsyncCrudAppService<TaskDto, Guid, TaskResultRequestDto, CreateTaskDto, TaskDto>
    {
    }
}


