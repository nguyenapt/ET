using System;
using Abp.Application.Services;
using ET.LeaveTypes.Dto;
using ET.SoW.Dto;

namespace ET.LeaveTypes
{
    public interface ILeaveTypeAppService : IAsyncCrudAppService<LeaveTypeDto, Guid, LeaveTypeResultRequestDto, CreateLeaveTypeDto, LeaveTypeDto>
    {
    }
}


