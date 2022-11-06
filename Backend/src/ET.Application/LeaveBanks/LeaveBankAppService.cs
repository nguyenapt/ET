using System;
using System.Linq;
using System.Threading.Tasks;
using ET.LeaveBanks.Dto;
using ET.Entities;
using ET.Sessions;
using Abp.UI;
using ET.Resources;
using ET.LeaveBanks.Repository;
using Abp.Domain.Repositories;
using Task = System.Threading.Tasks.Task;
using System.Collections;
using System.Collections.Generic;
using Abp.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace ET.LeaveBanks
{
    public class LeaveBankAppService : AsyncCrudAppService<LeaveBank, LeaveBankDto, Guid, LeaveBankDto, CreateLeaveBankDto, LeaveBankDto>, ILeaveBankAppService
    {
        private readonly SessionAppService _sessionAppService;

        public LeaveBankAppService(IRepository<LeaveBank, Guid> repository, SessionAppService sessionAppService) : base(repository)
        {
            _sessionAppService = sessionAppService;
        }

        public async Task<LeaveBank> GetByLeaveTypeAsync(int year, Guid leaveTypeId)
        {
            var loginInformation = await _sessionAppService.GetCurrentLoginInformations();
            if (loginInformation?.User == null)
            {
                throw new UserFriendlyException(404, "Cannot find current user");
            }
            var leaveBanks = await Repository.GetAllIncluding(x => x.Resource).FirstOrDefaultAsync(x => x.Resource.UserId == loginInformation.User.Id && x.LeaveTypeId == leaveTypeId && x.Year == year);            
            return leaveBanks;
        }
    }
}

