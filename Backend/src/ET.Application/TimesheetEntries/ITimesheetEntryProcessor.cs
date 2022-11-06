using System;
using ET.TimesheetEntries.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using ET.Email.Dto;

namespace ET.TimesheetEntries
{
    public interface ITimesheetEntryProcessor
    {
        Task InsertOrUpdateTimeSheetEntries(List<CreateOrUpdateTimeSheetEntryDto> timeSheets, long? userId);
        Task SubmitTimeSheets(List<Guid> timeSheetEntryIds);
        Task ApproveTimeSheets(List<Guid> timeSheetEntryIds);
        Task<List<SubmitTimesheetEmailDto>> GetSubmittedTimeSheetEmailInformation(List<Guid> timeSheetEntryIds, string userFullName);
        Task<ApproveOrRejectTimeSheetEmailDto> GetApprovedOrRejectedTimeSheetEmailInformation(List<Guid> timeSheetEntryIds, string approvalFullName, string comment = null);
        Task<bool> CanUserApproveOrRejectTheseTimeSheets(IEnumerable<Guid> timeSheetEntryIds, long userId);
        Task<bool> CheckWhetherTheseTimeSheetsContainSubmittedOrApprovedTimeSheet(IEnumerable<Guid> timeSheetEntryIds);
        Task<bool> CheckWhetherTheseTimeSheetsContainApprovedTimeSheet(IEnumerable<Guid> timeSheetEntryIds);
        Task<bool> CheckWhetherTheseTimeSheetsContainRejectedTimeSheet(IEnumerable<Guid> timeSheetEntryIds);
        Task<bool> CanUserSaveOrSubmitOrDeleteTheseTimeSheets(IEnumerable<Guid> timeSheetEntryIds, long userId);
        Task RejectTimeSheets(List<Guid> timeSheetEntryIds);
        Task<List<TimeSheetSubmitterCalendarDto>> GetTimeSheetSubmitterInformation(GetTimeSheetSubmitterInformationRequest request);
        List<TimeSheetEntryResponse> ToTimeSheetEntryResponse(IEnumerable<TimeSheetEntryFromAPeriodResponse> timeSheetEntryFromPeriod);
        List<CreateOrUpdateTimeSheetEntryDto> ToCreateOrUpdateTimeSheetEntryRequest(IEnumerable<CreateOrUpdateTimeSheetEntryRequestDto> timeSheetEntryFromPeriod);
        Task DeleteTimeSheets(List<Guid> timeSheetEntryIds);
    }
}