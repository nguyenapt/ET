using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ET.TimesheetEntries.Dto;
using ET.Sessions;
using Abp.UI;
using ET.Authorization.Users;
using ET.Email.Helper;
using ET.Resources;
using ET.TimesheetEntries.Exceptions;
using ET.TimesheetEntries.Repository;
using ET.Users.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace ET.TimesheetEntries
{
    public class TimesheetEntryAppService : ETAppServiceBase, ITimesheetEntryAppService
    {
        private readonly TimesheetEntryRepository _timeSheetEntryRepository;
        private readonly SessionAppService _sessionAppService;
        private readonly ITimesheetEntryProcessor _timeSheetEntryProcessor;
        private readonly IETEmailHelper _emailHelper;
        private readonly IResourceAppService _resourceAppService;

        public TimesheetEntryAppService(
            TimesheetEntryRepository timeSheetEntryRepository,
            SessionAppService sessionAppService,
            ITimesheetEntryProcessor timeSheetEntryProcessor,
            IETEmailHelper emailHelper,
            IResourceAppService resourceAppService,
            UserManager userManager)
        {
            _timeSheetEntryRepository = timeSheetEntryRepository;
            _sessionAppService = sessionAppService;
            _timeSheetEntryProcessor = timeSheetEntryProcessor;
            _emailHelper = emailHelper;
            _resourceAppService = resourceAppService;
        }

        public async Task SaveAsync(List<CreateOrUpdateTimeSheetEntryRequestDto> timeSheets)
        {
            if (timeSheets == null || !timeSheets.Any())
            {
                throw new ArgumentNullException(nameof(timeSheets), "TimeSheet are required");
            }

            var loginInformation = await _sessionAppService.GetCurrentLoginInformations();
            if (loginInformation?.User == null)
            {
                throw new UserNotFoundException("Cannot find current user");
            }

            if (timeSheets.Any(x => x.TimeSheetEntriesInDay == null || !x.TimeSheetEntriesInDay.Any()))
            {
                throw new UserFriendlyException(404, "Please make sure that you enter time sheet information in at least one day");
            }

            if (timeSheets.Any(x => x.TimeSheetEntriesInDay.Count > 7))
            {
                throw new UserFriendlyException(404, "A task cannot be submitted in more than 7 days");
            }

            if (timeSheets.Any(x => x.TimeSheetEntriesInDay.Any(y => y.Hours > 8 && y.IsOverTime == false)))
            {
                throw new UserFriendlyException(404, "Please select overtime for task taking more than 8 hours");
            }

            if (timeSheets.Where(timeSheet => timeSheet.IsLeaveType).Any(timeSheet => !timeSheet.LeaveTypeId.HasValue))
            {
                throw new UserFriendlyException(404, "Please add LeaveTypeId for Leave request");
            }

            var createOrUpdateTimeSheetRequest = _timeSheetEntryProcessor.ToCreateOrUpdateTimeSheetEntryRequest(timeSheets);

            var timeSheetEntryIds = createOrUpdateTimeSheetRequest.Where(x => x.TimeSheetEntryId.HasValue).Select(x => x.TimeSheetEntryId).ToList();
            if (timeSheetEntryIds.Any())
            {
                var ids = timeSheetEntryIds.Where(x => x.HasValue).Select(x => x.Value).ToArray();
                var canUserSaveThisTimeSheet = await _timeSheetEntryProcessor.CanUserSaveOrSubmitOrDeleteTheseTimeSheets(ids, loginInformation.User.Id);

                if (!canUserSaveThisTimeSheet)
                {
                    throw new SaveOrSubmitTimeSheetException("User cannot save this time sheet");
                }
            }

            await _timeSheetEntryProcessor.InsertOrUpdateTimeSheetEntries(createOrUpdateTimeSheetRequest, loginInformation.User.Id);
        }

        public async Task SubmitAsync(List<Guid> timeSheetEntryIds)
        {
            if (timeSheetEntryIds == null || !timeSheetEntryIds.Any())
            {
                throw new ArgumentNullException(nameof(timeSheetEntryIds), "TimeSheetEntry ids are required");
            }

            var loginInformation = await _sessionAppService.GetCurrentLoginInformations();
            if (loginInformation?.User == null)
            {
                throw new UserNotFoundException("Cannot find current user");
            }

            var canUserSubmitThisTimeSheet = await _timeSheetEntryProcessor.CanUserSaveOrSubmitOrDeleteTheseTimeSheets(timeSheetEntryIds, loginInformation.User.Id);

            if (!canUserSubmitThisTimeSheet)
            {
                throw new SaveOrSubmitTimeSheetException("User cannot submit this time sheet");
            }

            var doTimeSheetsContainSubmittedOrApprovedTimeSheet = await _timeSheetEntryProcessor
                .CheckWhetherTheseTimeSheetsContainSubmittedOrApprovedTimeSheet(timeSheetEntryIds);

            if (doTimeSheetsContainSubmittedOrApprovedTimeSheet)
            {
                throw new SaveOrSubmitTimeSheetException("Cannot submit these time sheets as there is at least one submitted or approved time sheets");
            }

            await _timeSheetEntryProcessor.SubmitTimeSheets(timeSheetEntryIds);

            var submitTimeSheetEmailsResult = await _timeSheetEntryProcessor.GetSubmittedTimeSheetEmailInformation(timeSheetEntryIds, $"{loginInformation.User.Name} {loginInformation.User.Surname}");

            var tasks = new List<Task>(); 
            foreach (var submitTimeSheetEmail in submitTimeSheetEmailsResult)
            {
                tasks.Add(_emailHelper.SendMailGenericAsync(ETConsts.ETEmailSettings.SubmitTimeSheet,
                new List<string> { submitTimeSheetEmail.ApprovalEmail },
                null,
                null,
                submitTimeSheetEmail,
                null));
            }

            await Task.WhenAll(tasks);
        }

        public async Task ApproveAsync(List<Guid> timeSheetEntryIds)
        {
            if (timeSheetEntryIds == null || !timeSheetEntryIds.Any())
            {
                throw new ArgumentNullException(nameof(timeSheetEntryIds), "TimeSheetEntry ids are required");
            }

            // Check whether this user can approve timesheets
            var loginInformation = await _sessionAppService.GetCurrentLoginInformations();
            if (loginInformation?.User == null)
            {
                throw new UserNotFoundException("Cannot find current user");
            }

            var canUserApproveOrRejectThisTimeSheet = await _timeSheetEntryProcessor.CanUserApproveOrRejectTheseTimeSheets(timeSheetEntryIds, loginInformation.User.Id);
            if (!canUserApproveOrRejectThisTimeSheet)
            {
                throw new ApproveOrRejectTimeSheetException("User cannot approve this time sheet");
            }

            var doTimeSheetsContainApprovedTimeSheet = await _timeSheetEntryProcessor
                .CheckWhetherTheseTimeSheetsContainApprovedTimeSheet(timeSheetEntryIds);

            if (doTimeSheetsContainApprovedTimeSheet)
            {
                throw new ApproveOrRejectTimeSheetException("Cannot approve these time sheets as there is at least one approved time sheet");
            }

            await _timeSheetEntryProcessor.ApproveTimeSheets(timeSheetEntryIds);
            
            // Base host link, todo get approve time sheet url
            
            var submitTimeSheetEmailsResult = await _timeSheetEntryProcessor.GetApprovedOrRejectedTimeSheetEmailInformation(timeSheetEntryIds, $"{loginInformation.User.Name} {loginInformation.User.Surname}");

            await _emailHelper.SendMailGenericAsync(ETConsts.ETEmailSettings.ApproveTimeSheet,
                new List<string> { submitTimeSheetEmailsResult.SubmitterEmail },
                null,
                null,
                submitTimeSheetEmailsResult,
                null).ConfigureAwait(false);
        }

        public async Task RejectAsync(RejectTimeSheetRequestDto rejectRequest)
        {
            // Check whether this user can reject time sheets
            var loginInformation = await _sessionAppService.GetCurrentLoginInformations();
            if (loginInformation?.User == null)
            {
                throw new UserNotFoundException("Cannot find current user");
            }

            var canUserApproveOrRejectThisTimeSheet = await _timeSheetEntryProcessor.CanUserApproveOrRejectTheseTimeSheets(rejectRequest.TimeSheetEntryIds, loginInformation.User.Id);
            if (!canUserApproveOrRejectThisTimeSheet)
            {
                throw new ApproveOrRejectTimeSheetException("User cannot reject this time sheet");
            }

            var doTimeSheetsContainRejectedTimeSheet = await _timeSheetEntryProcessor
                .CheckWhetherTheseTimeSheetsContainRejectedTimeSheet(rejectRequest.TimeSheetEntryIds);

            if (doTimeSheetsContainRejectedTimeSheet)
            {
                throw new SaveOrSubmitTimeSheetException("Cannot reject these time sheets as there is at least one rejected time sheet");
            }

            await _timeSheetEntryProcessor.RejectTimeSheets(rejectRequest.TimeSheetEntryIds);

            var rejectTimeSheetEmailsResult = await _timeSheetEntryProcessor.GetApprovedOrRejectedTimeSheetEmailInformation(rejectRequest.TimeSheetEntryIds, $"{loginInformation.User.Name} {loginInformation.User.Surname}", rejectRequest.Comment);

            await _emailHelper.SendMailGenericAsync(ETConsts.ETEmailSettings.RejectTimeSheet,
                new List<string> { rejectTimeSheetEmailsResult.SubmitterEmail },
                null,
                null,
                rejectTimeSheetEmailsResult,
                null).ConfigureAwait(false);
        }

        public async Task<List<TimeSheetEntryResponse>> GetTimeSheetsOfCurrentUserFromAPeriodAsync(TimePeriodDto timePeriod)
        {
            var loginInformation = await _sessionAppService.GetCurrentLoginInformations();
            if (loginInformation?.User == null)
            {
                throw new UserFriendlyException(404, "Cannot find current user");
            }

            var resource = await _resourceAppService.GetResourceByUserIdAsync(loginInformation.User.Id);

            var timeSheetForUserRequest = new GetTimeSheetsForUserRequestDto()
            {
                ResourceId = resource.Id,
                StartDate = timePeriod.StartDate, 
                EndDate = timePeriod.EndDate
            };

            var timeSheetEntryFromPeriod = await _timeSheetEntryRepository.GetTimeSheetForUserFromAPeriod(timeSheetForUserRequest);

            var result = _timeSheetEntryProcessor.ToTimeSheetEntryResponse(timeSheetEntryFromPeriod);
            return result;
        }

        public async Task<List<TimeSheetEntryResponse>> GetTimeSheetsDetailsForSubmitter(Guid resourceId, TimePeriodDto timePeriod)
        {
            var timeSheetForUserRequest = new GetTimeSheetsForUserRequestDto()
            {
                ResourceId = resourceId,
                StartDate = timePeriod.StartDate,
                EndDate = timePeriod.EndDate,
            };

            var timeSheetEntryFromPeriod = await _timeSheetEntryRepository.GetTimeSheetForUserFromAPeriod(timeSheetForUserRequest);
            return _timeSheetEntryProcessor.ToTimeSheetEntryResponse(timeSheetEntryFromPeriod);
        }

        public async Task<IEnumerable<TimeSheetSubmitterCalendarDto>> GetTimeSheetSubmittersForApproverAsync(int year, int month)
        {
            var loginInformation = await _sessionAppService.GetCurrentLoginInformations();
            if (loginInformation?.User == null)
            {
                throw new UserFriendlyException(404, "Cannot find current user");
            }

            var request = new GetTimeSheetSubmitterInformationRequest()
            {
                UserId = loginInformation.User.Id,
                Month = month,
                Year = year
            };

            return await _timeSheetEntryProcessor.GetTimeSheetSubmitterInformation(request);
        }

        public async Task WithDrawAsync(List<Guid> timeSheetEntryIds)
        {
            if (timeSheetEntryIds == null || !timeSheetEntryIds.Any())
            {
                throw new ArgumentNullException(nameof(timeSheetEntryIds), "TimeSheetEntry ids are required");
            }

            var loginInformation = await _sessionAppService.GetCurrentLoginInformations();
            if (loginInformation?.User == null)
            {
                throw new UserNotFoundException("Cannot find current user");
            }

            var canUserSubmitThisTimeSheet = await _timeSheetEntryProcessor.CanUserSaveOrSubmitOrDeleteTheseTimeSheets(timeSheetEntryIds, loginInformation.User.Id);
            if (!canUserSubmitThisTimeSheet)
            {
                throw new SaveOrSubmitTimeSheetException("User cannot submit this time sheet");
            }

            var doTimeSheetsContainSubmittedOrApprovedTimeSheet = await _timeSheetEntryProcessor
                .CheckWhetherTheseTimeSheetsContainSubmittedOrApprovedTimeSheet(timeSheetEntryIds);

            if (doTimeSheetsContainSubmittedOrApprovedTimeSheet)
            {
                throw new SaveOrSubmitTimeSheetException("Cannot delete these time sheets as there is at least one submitted or approved time sheets");
            }


            await _timeSheetEntryProcessor.DeleteTimeSheets(timeSheetEntryIds);
        }
    }
}
