using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using ET.LeaveTypes.Dto;
using ET.Entities;

namespace ET.LeaveTypes
{
    public class LeaveTypeAppService : AsyncCrudAppService<LeaveType, LeaveTypeDto, Guid, LeaveTypeResultRequestDto, CreateLeaveTypeDto, LeaveTypeDto>, ILeaveTypeAppService
    {
        public LeaveTypeAppService(IRepository<LeaveType, Guid> repository) : base(repository)
        {
        }

        protected override IQueryable<LeaveType> ApplySorting(IQueryable<LeaveType> query, LeaveTypeResultRequestDto input)
        {
            return query.OrderBy(x => x.Name);
        }     
    }
}

