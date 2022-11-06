using System;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using ET.Skills.Dto;
using ET.Entities;

namespace ET.Skills
{
    public class SkillAppService : AsyncCrudAppService<Skill, SkillDto, Guid, SkillResultRequestDto, CreateSkillDto, SkillDto>, ISkillAppService
    {
        public SkillAppService(IRepository<Skill, Guid> repository) : base(repository)
        {
        }

        //protected override IQueryable<SOW> CreateFilteredQuery(SkillResultRequestDto input)
        //{
        //    return Repository.GetAllIncluding(x => x.Skill, x => x.Skill, x => x.Project)
        //        .WhereIf(input.SkillId.HasValue, x => x.SkillId == input.SkillId.Value)
        //        .WhereIf(input.SkillId.HasValue, x => x.SkillId == input.SkillId.Value)
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

