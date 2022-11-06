using Abp.Data;
using Abp.EntityFrameworkCore;
using ET.Email.Dto;
using ET.Entities;
using ET.EntityFrameworkCore;
using ET.EntityFrameworkCore.Repositories;
using ET.TimesheetEntries.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = ET.Entities.Task;

namespace ET.TimesheetEntries.Repository
{
    public class TimesheetEntryRepository : ETRepositoryBase<TimesheetEntry, Guid>
    {
        private readonly IActiveTransactionProvider _transactionProvider;

        public TimesheetEntryRepository(IDbContextProvider<ETDbContext> dbContextProvider,
            IActiveTransactionProvider transactionProvider)
            : base(dbContextProvider, transactionProvider)
        {
            _transactionProvider = transactionProvider;
        }

        public async Task<List<TimeSheetEntryFromAPeriodResponse>> GetTimeSheetForUserFromAPeriod(GetTimeSheetsForUserRequestDto timeSheetForUserRequest)
        {
            if (timeSheetForUserRequest?.StartDate == null)
            {
                throw new ArgumentNullException(nameof(TimePeriodDto.StartDate));
            }

            if (timeSheetForUserRequest.EndDate == null)
            {
                throw new ArgumentNullException(nameof(TimePeriodDto.EndDate));
            }

            return await GetResultsByStoreProcedure<TimeSheetEntryFromAPeriodResponse, GetTimeSheetsForUserRequestDto>("dbo.spGetTimeSheetsForUserFromAPeriod", timeSheetForUserRequest);
        }

        public async Task<List<ResultCheckTimeSheetsResponseDto>> CheckWhetherTheseTimeSheetsContainSubmittedOrApprovedTimeSheets(TimeSheetsRequestDto dto)
        {
            if (dto?.TimeSheetEntryIds == null)
            {
                throw new ArgumentNullException(nameof(TimeSheetsRequestDto.TimeSheetEntryIds));
            }

            return await GetResultsByStoreProcedure<ResultCheckTimeSheetsResponseDto, TimeSheetsRequestDto>("dbo.spDoTimeSheetsContainSubmittedOrApprovedTimeSheet", dto);
        }

        public async Task<List<ResultCheckTimeSheetsResponseDto>> CheckWhetherTheseTimeSheetsContainApprovedTimeSheets(TimeSheetsRequestDto dto)
        {
            if (dto?.TimeSheetEntryIds == null)
            {
                throw new ArgumentNullException(nameof(TimeSheetsRequestDto.TimeSheetEntryIds));
            }

            return await GetResultsByStoreProcedure<ResultCheckTimeSheetsResponseDto,
                TimeSheetsRequestDto>("dbo.spDoTimeSheetsContainApprovedTimeSheet", dto);
        }
         
        public async Task<List<GetTimeSheetSubmitterInformationResponseDto>> GetTimeSheetSubmitterInformationForApprover(GetTimeSheetSubmitterInformationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(GetTimeSheetSubmitterInformationRequest));
            }

            return await GetResultsByStoreProcedure<GetTimeSheetSubmitterInformationResponseDto, GetTimeSheetSubmitterInformationRequest>("dbo.spGetTimeSheetSubmittersInformationForApprover", request);
        }

        public async Task<List<ApprovalInformationResponseDto>> GetApprovalInformation(ApprovalInformationRequestDto approvalRequest)
        {
            if (approvalRequest?.TimesheetEntryIds == null)
            {
                throw new ArgumentNullException(nameof(ApprovalInformationRequestDto.TimesheetEntryIds));
            }

            return await GetResultsByStoreProcedure<ApprovalInformationResponseDto, ApprovalInformationRequestDto>("dbo.spGetApprovalInformation", approvalRequest);
        }

        public async Task<List<ResultCheckTimeSheetsResponseDto>> CheckWhetherThisUserCanApproveOrRejectTimeSheet(CanUserApproveOrRejectThisTimeSheetRequestDto dto)
        {
            if (dto?.TimeSheetEntryIds == null)
            {
                throw new ArgumentNullException(nameof(CanUserApproveOrRejectThisTimeSheetRequestDto.TimeSheetEntryIds));
            }

            return await GetResultsByStoreProcedure<ResultCheckTimeSheetsResponseDto, CanUserApproveOrRejectThisTimeSheetRequestDto>("dbo.spCanUserApproveOrRejectTimeSheet", dto);
        }

        public async Task<List<ResultCheckTimeSheetsResponseDto>> CheckWhetherTheseTimeSheetsContainRejectedTimeSheets(TimeSheetsRequestDto dto)
        {
            if (dto?.TimeSheetEntryIds == null)
            {
                throw new ArgumentNullException(nameof(CanUserApproveOrRejectThisTimeSheetRequestDto.TimeSheetEntryIds));
            }

            return await GetResultsByStoreProcedure<ResultCheckTimeSheetsResponseDto, TimeSheetsRequestDto>("dbo.spDoTimeSheetsContainRejectedTimeSheet", dto);
        }

        public async Task<List<CanUserSaveOrSubmitTimeSheetResponseDto>> CheckWhetherThisUserCanSaveOrSubmitTimeSheet(
            CanUserSaveOrSubmitTimeSheetRequestDto dto)
        {
            if (dto?.TimeSheetEntryIds == null)
            {
                throw new ArgumentNullException(nameof(CanUserSaveOrSubmitTimeSheetRequestDto.TimeSheetEntryIds));
            }

            return await GetResultsByStoreProcedure<CanUserSaveOrSubmitTimeSheetResponseDto, CanUserSaveOrSubmitTimeSheetRequestDto>("dbo.spCanUserSaveOrSubmitTimeSheet", dto);
        }
  
        public async Task<List<SubmitterTimeSheetInformationDto>> GetApprovedOrRejectedTimeSheetInformation(GetApprovedOrRejectedTimeSheetInformationRequestDto approvalRequest)
        {
            if (approvalRequest?.TimeSheetEntryIds == null)
            {
                throw new ArgumentNullException(nameof(GetApprovedOrRejectedTimeSheetInformationRequestDto.TimeSheetEntryIds));
            }

            return await GetResultsByStoreProcedure<SubmitterTimeSheetInformationDto, GetApprovedOrRejectedTimeSheetInformationRequestDto>("dbo.spGetSubmitterInformationOfTimeSheet", approvalRequest);
        }

        public async Task<List<GetTimeSheetSubmitterInformationResponseDto>> GetTimeSheetCalendarForCurrentUser(GetTimeSheetSubmitterInformationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(GetTimeSheetSubmitterInformationRequest));
            }

            return await GetResultsByStoreProcedure<GetTimeSheetSubmitterInformationResponseDto, GetTimeSheetSubmitterInformationRequest>("dbo.spGetTimeSheetCalendarForCurrentUser", request);
        }
    }
}
