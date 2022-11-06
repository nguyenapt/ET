using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using ET.Tasks.Dto;
using ET.Entities;

namespace ET.Tasks
{
    public class TaskAppService : AsyncCrudAppService<Entities.Task, TaskDto, Guid, TaskResultRequestDto, CreateTaskDto, TaskDto>, ITaskAppService
    {
        public TaskAppService(IRepository<Entities.Task, Guid> repository) : base(repository)
        {
        }

        //protected override IQueryable<SOW> CreateFilteredQuery(TaskResultRequestDto input)
        //{
        //    return Repository.GetAllIncluding(x => x.Task, x => x.Task, x => x.Project)
        //        .WhereIf(input.TaskId.HasValue, x => x.TaskId == input.TaskId.Value)
        //        .WhereIf(input.TaskId.HasValue, x => x.TaskId == input.TaskId.Value)
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

