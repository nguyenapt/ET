using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ET.Entities;
using ET.LeavePermissions.Dto;
using ET.LeaveTypes.Dto;

namespace ET.LeavePermissions
{
    public interface ILeavePermissionAppService
    {
        Task<LeavePermission> CreateAsync(CreateLeavePermissionDto input);
        Task<LeavePermission> Approve(Guid id);
        Task<LeavePermission> Reject(Guid id, string rejectReason);
        Task<IEnumerable<LeavePermissionDto>> GetByDateAsync(DateTime date, Guid resourceId);
        Task<List<LeaveTypeDto>> GetRemainingLeaveType();
    }
}


