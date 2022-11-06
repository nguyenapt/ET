using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ET.TaskCategorys.Dto;
using ET.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ET.TaskCategorys
{
    public class TaskCategoryAppService : AsyncCrudAppService<TaskCategory, TaskCategoryDto, Guid, TaskCategoryResultRequestDto, CreateTaskCategoryDto, TaskCategoryDto>, ITaskCategoryAppService
    {
        IRepository<Allocation, Guid> _allocationRepository;
        public TaskCategoryAppService(IRepository<TaskCategory, Guid> repository, IRepository<Allocation, Guid> allocationRepository) : base(repository)
        {
            _allocationRepository = allocationRepository;
        }

        public async Task<IEnumerable<TaskCategoryDto>> GetTaskCategoriesByAllocation(Guid id)
        {
            var allocation = await _allocationRepository.GetAllIncluding(x => x.SOWRole).FirstOrDefaultAsync(x => x.Id == id);
            var categories = Repository.GetAll().AsEnumerable().Where(x => string.IsNullOrEmpty(x.RoleName) || x.RoleName == allocation.SOWRole.RoleName);
            return await System.Threading.Tasks.Task.FromResult(ObjectMapper.Map<IEnumerable<TaskCategoryDto>>(categories));
        }

        //protected override IQueryable<SOW> CreateFilteredQuery(TaskCategoryResultRequestDto input)
        //{
        //    return Repository.GetAllIncluding(x => x.TaskCategory, x => x.TaskCategory, x => x.Project)
        //        .WhereIf(input.TaskCategoryId.HasValue, x => x.TaskCategoryId == input.TaskCategoryId.Value)
        //        .WhereIf(input.TaskCategoryId.HasValue, x => x.TaskCategoryId == input.TaskCategoryId.Value)
        //        .WhereIf(input.ProjectId.HasValue, x => x.ProjectId == input.ProjectId.Value)
        //        .WhereIf(input.Status.HasValue, x => x.Status == input.Status.Value)
        //        .WhereIf(input.StartDate.HasValue, x => x.StartDate >= input.StartDate.Value)
        //        .WhereIf(input.EndDate.HasValue, x => x.EndDate <= input.StartDate.Value)
        //        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword));
        //    //.WhereIf(!input.BillType.IsNullOrWhiteSpace(), x => x.bill == input.Status.Value)
        //}

        //protected override IQueryable<SOW> ApplySorting(IQueryable<SOW> query, SowResultRequestDto input)
        //{
        //    // TODO: If font-end require sortoption -> add it as propert to the input dto then handle here
        //    return query.OrderByDescending(x => x.CreatedTime);
        //}

        //protected override IQueryable<SOW> ApplyPaging(IQueryable<SOW> query, SowResultRequestDto input)
        //{
        //    return query.Skip((input.CurrentPage - 1) * input.PageSize).Take(input.PageSize);
        //}
    }
}

