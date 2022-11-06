using Abp.Application.Services;
using ET.Entities;
using ET.LeaveBanks.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ET.LeaveBanks
{
    public interface ILeaveBankAppService : IAsyncCrudAppService<LeaveBankDto, Guid, LeaveBankDto, CreateLeaveBankDto, LeaveBankDto>
    {
       Task<LeaveBank> GetByLeaveTypeAsync(int year, Guid leaveTypeId);
    }
}


