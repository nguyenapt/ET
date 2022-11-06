using System;
using Abp.Application.Services;
using ET.TaskCategorys.Dto;
using ET.SoW.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ET.TaskCategorys
{
    public interface ITaskCategoryAppService : IAsyncCrudAppService<TaskCategoryDto, Guid, TaskCategoryResultRequestDto, CreateTaskCategoryDto, TaskCategoryDto>
    {
        Task<IEnumerable<TaskCategoryDto>> GetTaskCategoriesByAllocation(Guid id);
    }
}


