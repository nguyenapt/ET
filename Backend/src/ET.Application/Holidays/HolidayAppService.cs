using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using ET.Holidays.Dto;
using ET.Entities;

namespace ET.Holidays
{
    public class HolidayAppService : AsyncCrudAppService<Holiday, HolidayDto, Guid, HolidayResultRequestDto, CreateHolidayDto, HolidayDto>, IHolidayAppService
    {
        public HolidayAppService(IRepository<Holiday, Guid> repository) : base(repository)
        {
        }

        //protected override IQueryable<SOW> CreateFilteredQuery(HolidayResultRequestDto input)
        //{
        //    return Repository.GetAllIncluding(x => x.Holiday, x => x.Holiday, x => x.Project)
        //        .WhereIf(input.HolidayId.HasValue, x => x.HolidayId == input.HolidayId.Value)
        //        .WhereIf(input.HolidayId.HasValue, x => x.HolidayId == input.HolidayId.Value)
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

