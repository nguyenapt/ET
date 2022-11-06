using System;
using System.Linq;
using System.Threading.Tasks;
using ET.LeavePermissions.Dto;
using ET.Entities;
using ET.TimesheetEntries.Repository;
using System.Collections.Generic;
using ET.Sessions;
using Abp.UI;
using ET.Resources;
using ET.TimesheetEntries.Dto;
using ET.LeavePermissions.Repository;
using ET.Email.Helper;
using ET.Email.Dto;
using Abp.Domain.Repositories;
using ET.LeaveTypes.Dto;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
using Microsoft.Extensions.Configuration;
using Abp.Extensions;
using ET.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ET.LeaveBanks;

namespace ET.LeavePermissions
{
    public class LeavePermissionAppService : ETAppServiceBase, ILeavePermissionAppService
    {
        private readonly TimesheetEntryRepository _timeSheetEntryRepository;
        private readonly SessionAppService _sessionAppService;
        private readonly IResourceAppService _resourceAppService;
        private readonly LeavePermissionRepository _leavePermissionRepository;
        private readonly IETEmailHelper _emailHelper;
        protected readonly IRepository<Resource, Guid> _resourceRepository;
        protected readonly IRepository<LeaveType, Guid> _leaveTypeRepository;
        private readonly LeaveBankAppService _leaveBankAppService;
        private IConfigurationRoot _appConfiguration;

        public LeavePermissionAppService(LeavePermissionRepository leavePermissionRepository, TimesheetEntryRepository timeSheetEntryRepository, SessionAppService sessionAppService, IResourceAppService resourceAppService, IETEmailHelper emailHelper, IRepository<Resource, Guid> resourceRepository, IRepository<LeaveType, Guid> leaveTypeRepository, IWebHostEnvironment webHostEnvironment, LeaveBankAppService leaveBankAppService)
        {
            _timeSheetEntryRepository = timeSheetEntryRepository;
            _sessionAppService = sessionAppService;
            _resourceAppService = resourceAppService;
            _leavePermissionRepository = leavePermissionRepository;
            _emailHelper = emailHelper;
            _resourceRepository = resourceRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveBankAppService = leaveBankAppService;
            _appConfiguration = AppConfigurations.Get(webHostEnvironment.ContentRootPath, webHostEnvironment.EnvironmentName, webHostEnvironment.IsDevelopment());
        }

        public async Task<List<LeaveTypeDto>> GetRemainingLeaveType()
        {
            var loginInformation = await _sessionAppService.GetCurrentLoginInformations();
            if (loginInformation?.User == null)
            {
                throw new UserFriendlyException(404, "Cannot find current user");
            }
            var leaveTypes = new List<LeaveTypeDto>();
            var resource = await _resourceAppService.GetResourceByUserIdAsync(loginInformation.User.Id);
            var allLeaveTypes = await _leaveTypeRepository.GetAllListAsync();
            var allLeaverPermissions = _leavePermissionRepository.GetAll().Where(x => x.ResourceId == resource.Id);
            foreach (var leave in allLeaveTypes)
            {
                var leaveBank = await _leaveBankAppService.GetByLeaveTypeAsync(DateTime.Now.Year, leave.Id);
                var totalLeaveAllowed = leaveBank != null ? leaveBank.TotalAllowedHours : 96;
                var totalLeaveHours = allLeaverPermissions.Where(x => x.LeaveTypeId == leave.Id && (x.ApprovalStatus == (byte)ApproveStatus.Approved || x.ApprovalStatus == (byte)ApproveStatus.WaitingForApproval)).Sum(x => x.TotalHours);
                var leaveType = ObjectMapper.Map<LeaveTypeDto>(leave);
                leaveType.RemainingLeave = totalLeaveAllowed - totalLeaveHours;
                leaveType.TotalAllowedLeave = totalLeaveAllowed;
                leaveTypes.Add(leaveType);
            }

            return await Task.FromResult(leaveTypes.OrderBy(x => x.Name).ToList());
        }

        public async Task<LeavePermissionDto> GetAsync(Guid id)
        {
            var leaverPermission = await _leavePermissionRepository.GetAllIncluding(x => x.LeaveType, x => x.Resource, x => x.TimesheetEntries).FirstOrDefaultAsync(x => x.Id == id);

            return ObjectMapper.Map<LeavePermissionDto>(leaverPermission);
        }

        public Task<IEnumerable<LeavePermissionDto>> GetByDateAsync(DateTime date, Guid resourceId)
        {
            var leaverPermissions = _leavePermissionRepository.GetAllIncluding(x => x.LeaveType, x => x.Resource).Where(x => x.ResourceId == resourceId && (x.StartDate.Date == date || (x.IsFullDay && x.StartDate.Date <= date && x.EndDate.Date >= date)));

            return Task.FromResult(ObjectMapper.Map<IEnumerable<LeavePermissionDto>>(leaverPermissions));
        }

        public async Task<IEnumerable<CalendarLeavePermissionDto>> GetAllAsync(int month, int year)
        {
            var loginInformation = await _sessionAppService.GetCurrentLoginInformations();
            if (loginInformation?.User == null)
            {
                throw new UserFriendlyException(404, "Cannot find current user");
            }
            var resource = await _resourceAppService.GetResourceByUserIdAsync(loginInformation.User.Id);

            var leaves = _timeSheetEntryRepository.GetAllIncluding(x => x.LeavePermission, x => x.LeavePermission.Resource)
                .Where(x => x.LeavePermissionId.HasValue && 
                (x.ApprovalId == resource.Id || x.LeavePermission.ResourceId == resource.Id) 
                && x.RecordDate.Year == year && x.RecordDate.Month == month).ToList()
                .GroupBy(x => x.LeavePermission.Resource)
                .Select(x => new CalendarLeavePermissionDto() 
                { ResourceName = x.Key.FirstName + " " + x.Key.LastName, ResourceId = x.Key.Id, CalendarItemLeavePermissions = 
                x.Select(y => new CalendarItemLeavePermissionDto() { Date = y.RecordDate, Hours = y.Hours, ApproveStatus = y.ApprovalStatus.HasValue ? ((ApproveStatus)y.ApprovalStatus.Value).ToString() : null, 
                LeavePermissionId = y.LeavePermissionId.Value }) }).ToList();

            if (leaves.FirstOrDefault(x => x.ResourceId == resource.Id) == null)
            {
                leaves.Add(new CalendarLeavePermissionDto()
                {
                    ResourceId = resource.Id,
                    ResourceName = $"{resource.FirstName} {resource.LastName}",
                });
            }

            return await Task.FromResult(leaves);
        }

        public async Task<LeavePermission> CreateAsync(CreateLeavePermissionDto input)
        {
            var loginInformation = await _sessionAppService.GetCurrentLoginInformations();
            if (loginInformation?.User == null)
            {
                throw new UserFriendlyException(404, "Cannot find current user");
            }

            if (input.EndDate < input.StartDate)
            {
                throw new UserFriendlyException(404, "EndDate must be greater than StartDate");
            }

            if (input.StartDate.DayOfWeek == DayOfWeek.Saturday || input.StartDate.DayOfWeek == DayOfWeek.Sunday || input.EndDate.DayOfWeek == DayOfWeek.Saturday || input.EndDate.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new UserFriendlyException(404, "You cannot book leaves on Saturday or Sunday");
            }

            var resource = await _resourceAppService.GetResourceByUserIdAsync(loginInformation.User.Id);

            var isOverlappedLeave = _leavePermissionRepository.GetAll().Where(x => x.ResourceId == resource.Id && x.ApprovalStatus != (byte)ApproveStatus.Rejected).AsEnumerable().Count(x => OverlappingPeriods(input.StartDate, input.EndDate, x.StartDate, x.EndDate)) > 0;
            if (isOverlappedLeave)
            {
                throw new UserFriendlyException(404, "There are leave on your selected period, please review again!");
            }

            var leavePermission = ObjectMapper.Map<LeavePermission>(input);

            if (input.IsFullDay)
            {
                var days = (input.EndDate - input.StartDate).Days + 1;
                leavePermission.TotalHours = days * 8;
            }
            else
            {
                leavePermission.TotalHours = (input.EndDate - input.StartDate).Hours;
            }

            // Insert TimesheetEntry
            var timesheetEntries = new List<TimesheetEntry>();
            for (var day = input.StartDate; day.Date <= input.EndDate; day = day.AddDays(1))
            {
                var timeSheetEntry = new TimesheetEntry()
                {
                    Hours = input.IsFullDay ? 8 : leavePermission.TotalHours,
                    RecordDate = day.Date,
                    ApprovalId = input.ApprovalId,
                    SubmittedTimestamp = DateTime.UtcNow,
                    ApprovalStatus = (byte?)ApproveStatus.WaitingForApproval
                };
                timesheetEntries.Add(timeSheetEntry);
                await _timeSheetEntryRepository.InsertAsync(timeSheetEntry);
            }

            leavePermission.TimesheetEntries = timesheetEntries;
            leavePermission.ResourceId = resource.Id;
            leavePermission.CreationTime = DateTime.UtcNow;
            leavePermission.ApprovalStatus = (byte?)ApproveStatus.WaitingForApproval;

            await _leavePermissionRepository.InsertOrUpdateAsync(leavePermission);

            //notify approval user

            //if (leavePermission.Id != Guid.Empty && input.ApprovalId.HasValue)
            if (leavePermission.Id != Guid.Empty && input.NotificationIds.Any())
            {
                foreach(var approvalId in input.NotificationIds)
                {
                    //var approvalUser = await _resourceRepository.GetAllIncluding(x => x.User).FirstOrDefaultAsync(x => x.Id == input.ApprovalId);
                    var approvalUser = await _resourceRepository.GetAllIncluding(x => x.User).FirstOrDefaultAsync(x => x.Id.ToString() == approvalId.ToString());
                    var hostLink = _appConfiguration["App:ClientRootAddress"].EnsureEndsWith('/') + "time-entry/approve-leave/" + leavePermission.Id;
                    var submitLeavePermissionEmail = new SubmitLeavePermissionEmailDto
                    {
                        SubmitterFullName = resource.User.FullName,
                        ApprovalEmail = approvalUser.User.EmailAddress,
                        ApprovalFullName = approvalUser.User.FullName,
                        ApproveTimeSheetLink = hostLink,
                        LeaveFromTime = leavePermission.StartDate.ToString(leavePermission.IsFullDay ? "dd/MM/yyyy" : "dd/MM/yyyy HH:mm"),
                        LeaveToTime = leavePermission.EndDate.ToString(leavePermission.IsFullDay ? "dd/MM/yyyy" : "dd/MM/yyyy HH:mm"),
                        LeaveType = leavePermission.IsFullDay ? "Full Day" : "Partial Day",
                        Reason = leavePermission.Reason,
                        EmailSupport = "cuc.nguyen@Provide it here.se",
                        SkypeSupport = "cuc.nguyen"
                    };

                    _emailHelper.SendMailAsync(ETConsts.ETEmailSettings.SubmitLeavePermission,
                   new List<string> { submitLeavePermissionEmail.ApprovalEmail },
                   null,
                   null,
                   submitLeavePermissionEmail,
                   null);
                }             
            }

            return await Task.FromResult(leavePermission);
        }

        public Task<LeavePermission> Approve(Guid id)
        {
            var leavePermission = _leavePermissionRepository.GetAllIncluding(x => x.TimesheetEntries, x => x.Resource, x => x.Resource.User).FirstOrDefault(x => x.Id == id);
            if (leavePermission.ApprovalStatus != (byte?)ApproveStatus.WaitingForApproval) return Task.FromResult(leavePermission);
            leavePermission.ApprovalStatus = (byte?)ApproveStatus.Approved;
            leavePermission.LastModificationTime = DateTime.UtcNow;

            Guid? approvalId = null;
            foreach (var entry in leavePermission.TimesheetEntries)
            {
                entry.ApprovalStatus = (byte?)ApproveStatus.Approved;
                _timeSheetEntryRepository.UpdateAsync(entry);
                if (approvalId == null) approvalId = entry.ApprovalId;
            }
            //notify submitter
            if (approvalId.HasValue)
            {
                var approvalUser = _resourceRepository.GetAllIncluding(x => x.User).FirstOrDefault(x => x.Id == approvalId);
                var hostLink = _appConfiguration["App:ClientRootAddress"].EnsureEndsWith('/') + "time-entry/approve-leave/" + id.ToString();
                var submitLeavePermissionEmail = new SubmitLeavePermissionEmailDto
                {
                    SubmitterFullName = leavePermission.Resource.FirstName + " " + leavePermission.Resource.LastName,
                    ApprovalEmail = approvalUser.User.EmailAddress,
                    ApprovalFullName = approvalUser.User.FullName,
                    ApproveTimeSheetLink = hostLink,
                    LeaveFromTime = leavePermission.StartDate.ToString(leavePermission.IsFullDay ? "dd/MM/yyyy" : "dd/MM/yyyy HH:mm"),
                    LeaveToTime = leavePermission.EndDate.ToString(leavePermission.IsFullDay ? "dd/MM/yyyy" : "dd/MM/yyyy HH:mm"),
                    LeaveType = leavePermission.IsFullDay ? "Full Day" : "Partial Day",
                    Reason = leavePermission.Reason,
                    EmailSupport = "cuc.nguyen@Provide it here.se",
                    SkypeSupport = "cuc.nguyen"
                };
                _emailHelper.SendMailGenericAsync(ETConsts.ETEmailSettings.ApproveLeavePermission,
                new List<string> { leavePermission.Resource.User.EmailAddress },
                null,
                null,
                submitLeavePermissionEmail,
                null);
            }
            return _leavePermissionRepository.UpdateAsync(leavePermission);
        }

        public Task<LeavePermission> Reject(Guid id, string rejectReason)
        {
            var leavePermission = _leavePermissionRepository.GetAllIncluding(x => x.TimesheetEntries, x => x.Resource, x => x.Resource.User).FirstOrDefault(x => x.Id == id);
            if (leavePermission.ApprovalStatus != (byte?)ApproveStatus.WaitingForApproval) return Task.FromResult(leavePermission);
            leavePermission.ApprovalStatus = (byte?)ApproveStatus.Rejected;
            leavePermission.LastModificationTime = DateTime.UtcNow;
            leavePermission.RejectReason = rejectReason;

            Guid? approvalId = null;
            foreach (var entry in leavePermission.TimesheetEntries)
            {
                entry.ApprovalStatus = (byte?)ApproveStatus.Rejected;
                _timeSheetEntryRepository.UpdateAsync(entry);
                if (approvalId == null) approvalId = entry.ApprovalId;
            }
            //notify submitter
            if (approvalId.HasValue)
            {
                var approvalUser = _resourceRepository.GetAllIncluding(x => x.User).FirstOrDefault(x => x.Id == approvalId);
                var hostLink = _appConfiguration["App:ClientRootAddress"].EnsureEndsWith('/') + "time-entry/approve-leave/" + id.ToString();
                var submitLeavePermissionEmail = new SubmitLeavePermissionEmailDto
                {
                    SubmitterFullName = leavePermission.Resource.FirstName + " " + leavePermission.Resource.LastName,
                    ApprovalEmail = approvalUser.User.EmailAddress,
                    ApprovalFullName = approvalUser.User.FullName,
                    ApproveTimeSheetLink = hostLink,
                    LeaveFromTime = leavePermission.StartDate.ToString(leavePermission.IsFullDay ? "dd/MM/yyyy" : "dd/MM/yyyy HH:mm"),
                    LeaveToTime = leavePermission.EndDate.ToString(leavePermission.IsFullDay ? "dd/MM/yyyy" : "dd/MM/yyyy HH:mm"),
                    LeaveType = leavePermission.IsFullDay ? "Full Day" : "Partial Day",
                    RejectReason = leavePermission.RejectReason,
                    EmailSupport = "cuc.nguyen@Provide it here.se",
                    SkypeSupport = "cuc.nguyen"
                };
                _emailHelper.SendMailGenericAsync(ETConsts.ETEmailSettings.RejectLeavePermission,
                new List<string> { leavePermission.Resource.User.EmailAddress },
                null,
                null,
                submitLeavePermissionEmail,
                null);
            }
            return _leavePermissionRepository.UpdateAsync(leavePermission);
        }

        private bool OverlappingPeriods(DateTime aStart, DateTime aEnd, DateTime bStart, DateTime bEnd)
        {
            if (aStart > aEnd)
                return false;
            // throw new ArgumentException("A start can not be after its end.");

            if (bStart > bEnd)
                return false;
            //throw new ArgumentException("B start can not be after its end.");

            if (bStart == bEnd)
                return !((aEnd.Date < bStart.Date && aStart.Date < bStart.Date) ||
                   (bEnd.Date < aStart.Date && bStart.Date < aStart.Date));

            return !((aEnd < bStart && aStart < bStart) ||
                     (bEnd < aStart && bStart < aStart));
        }
    }
}

