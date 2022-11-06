using Abp.Dependency;
using Abp.Domain.Repositories;
using ET.Email.Dto;
using ET.Entities;
using ET.TimesheetEntries.Dto;
using ET.TimesheetEntries.Exceptions;
using ET.TimesheetEntries.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.UI;
using ET.Configuration;
using ET.Resources;
using ET.TaskCategorys.Dto;
using ET.Tasks.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Task = System.Threading.Tasks.Task;

namespace ET.TimesheetEntries
{
    public class TimesheetEntryProcessor : ITransientDependency, ITimesheetEntryProcessor
    {
        private readonly TimesheetEntryRepository _timeSheetEntryRepository;
        private readonly IRepository<LeavePermission, Guid> _leavePermissionRepository;
        private readonly ResourceAppService _resourceAppService;
        private readonly IConfigurationRoot _appConfiguration;

        public TimesheetEntryProcessor(
            TimesheetEntryRepository timeSheetEntryRepository,
            IRepository<LeavePermission, Guid> leavePermissionRepository,
            ResourceAppService resourceAppService,
            IWebHostEnvironment webHostEnvironment)
        {
            _timeSheetEntryRepository = timeSheetEntryRepository;
            _leavePermissionRepository = leavePermissionRepository;
            _resourceAppService = resourceAppService;
            _appConfiguration = AppConfigurations.Get(webHostEnvironment.ContentRootPath, webHostEnvironment.EnvironmentName, webHostEnvironment.IsDevelopment()); ;
        }

        public async Task InsertOrUpdateTimeSheetEntries(List<CreateOrUpdateTimeSheetEntryDto> timeSheets, long? userId)
        {
            if (timeSheets == null || !timeSheets.Any())
            {
                throw new ArgumentNullException(nameof(CreateOrUpdateTimeSheetEntryDto));
            }

            var resource = new Resource();

            if (timeSheets.Select(x => x.IsLeaveType).Any() && userId != null)
            {
                resource = await _resourceAppService.GetResourceByUserIdAsync(userId.Value);
            }

            foreach (var timeSheet in timeSheets)
            {
                var leaveId = timeSheet.LeavePermissionId;

                // Create leave permission
                if (timeSheet.IsLeaveType && !timeSheet.LeavePermissionId.HasValue && timeSheet.LeaveTypeId.HasValue)
                {
                    var leave = new LeavePermission
                    {
                        ResourceId = resource.Id,
                        CreationTime = DateTime.UtcNow,
                        StartDate = timeSheet.RecordDate,
                        EndDate = timeSheet.RecordDate,
                        IsFullDay = timeSheet.Hours == 8,
                        Reason = timeSheet.Description,
                        LeaveTypeId = timeSheet.LeaveTypeId.Value,
                        ApprovalStatus = (byte?)ApproveStatus.WaitingForApproval
                    };

                    leaveId = await _leavePermissionRepository.InsertAndGetIdAsync(leave);
                }

                // Update
                if (timeSheet.TimeSheetEntryId.HasValue)
                {
                    var timeSheetEntryEdit = await _timeSheetEntryRepository.GetAsync(timeSheet.TimeSheetEntryId.Value);
                    if (timeSheetEntryEdit == null)
                    {
                        throw new TimesheetEntryNotFoundException($"Cannot find TimeSheet with Id {timeSheet.TimeSheetEntryId}");
                    }
                    
                    timeSheetEntryEdit.AllocationId = timeSheet.AllocationId;
                    timeSheetEntryEdit.TaskId = timeSheet.TaskId;
                    timeSheetEntryEdit.TicketName = timeSheet.TicketName;
                    timeSheetEntryEdit.Description = timeSheet.Description;
                    timeSheetEntryEdit.Hours = timeSheet.Hours;
                    timeSheetEntryEdit.RecordDate = timeSheet.RecordDate.Date;
                    timeSheetEntryEdit.isOverTime = timeSheet.IsOverTime;
                    timeSheetEntryEdit.ApprovalId = timeSheet.ApprovalId;
                    timeSheetEntryEdit.ApprovalStatus = null;
                    timeSheetEntryEdit.LeavePermissionId = timeSheet.IsLeaveType ? leaveId : null;
                    
                    await _timeSheetEntryRepository.UpdateAsync(timeSheetEntryEdit);
                    continue;
                }

                // Insert
                var timeSheetEntry = new TimesheetEntry()
                {
                    AllocationId = timeSheet.AllocationId,
                    TaskId = timeSheet.TaskId,
                    Description = timeSheet.Description,
                    Hours = timeSheet.Hours,
                    RecordDate = timeSheet.RecordDate.Date,
                    isOverTime = timeSheet.IsOverTime,
                    ApprovalId = timeSheet.ApprovalId,
                    TicketName = timeSheet.TicketName,
                    ApprovalStatus = null,
                    LeavePermissionId = timeSheet.IsLeaveType ? leaveId : null
                };

                 await _timeSheetEntryRepository.InsertAsync(timeSheetEntry);
            }
        }

        public async Task SubmitTimeSheets(List<Guid> timeSheetEntryIds)
        {
            foreach (var id in timeSheetEntryIds)
            {
                var timeSheetEntry = await _timeSheetEntryRepository.GetAsync(id);
                if (timeSheetEntry == null)
                {
                    throw new TimesheetEntryNotFoundException($"Cannot find TimeSheet with Id {id}");
                }

                if (timeSheetEntry.ApprovalId == null)
                {
                    throw new ApprovalRequiredException($"Approval of timeSheet with Id: {timeSheetEntry.Id} is required");
                }

                if (timeSheetEntry.LeavePermission != null)
                {
                    timeSheetEntry.LeavePermission.ApprovalStatus = (byte?)ApproveStatus.WaitingForApproval;
                }
    
                timeSheetEntry.ApprovalStatus = (byte?) ApproveStatus.WaitingForApproval;
                timeSheetEntry.SubmittedTimestamp = DateTime.UtcNow;
                await _timeSheetEntryRepository.UpdateAsync(timeSheetEntry);
            }
        }

        public async Task ApproveTimeSheets(List<Guid> timeSheetEntryIds)
        {
            foreach (var id in timeSheetEntryIds)
            {
                var timeSheetEntry = await _timeSheetEntryRepository.GetAsync(id);
                if (timeSheetEntry == null)
                {
                    throw new TimesheetEntryNotFoundException($"Cannot find TimeSheet with Id {id}");
                }

                if (timeSheetEntry.ApprovalId == null)
                {
                    throw new ApprovalRequiredException($"Approval of timeSheet with Id: {timeSheetEntry.Id} is required");
                }

                if (timeSheetEntry.LeavePermission != null)
                {
                    timeSheetEntry.LeavePermission.ApprovalStatus = (byte?)ApproveStatus.Approved;
                }

                timeSheetEntry.ApprovalStatus = (byte?)ApproveStatus.Approved;
                timeSheetEntry.ApprovedTimestamp = DateTime.UtcNow;

                await _timeSheetEntryRepository.UpdateAsync(timeSheetEntry);
            }
        }

        public async Task RejectTimeSheets(List<Guid> timeSheetEntryIds)
        {
            foreach (var id in timeSheetEntryIds)
            {
                var timeSheetEntry = await _timeSheetEntryRepository.GetAsync(id);
                if (timeSheetEntry == null)
                {
                    throw new TimesheetEntryNotFoundException($"Cannot find TimeSheet with Id {id}");
                }

                if (timeSheetEntry.ApprovalId == null)
                {
                    throw new ApprovalRequiredException($"Approval of timeSheet with Id: {timeSheetEntry.Id} is required");
                }

                if (timeSheetEntry.LeavePermission != null)
                {
                    timeSheetEntry.LeavePermission.ApprovalStatus = (byte?)ApproveStatus.Rejected;
                }

                timeSheetEntry.ApprovalStatus = (byte?)ApproveStatus.Rejected;
                timeSheetEntry.ApprovedTimestamp = DateTime.UtcNow;

                await _timeSheetEntryRepository.UpdateAsync(timeSheetEntry);
            }
        }

        public async Task DeleteTimeSheets(List<Guid> timeSheetEntryIds)
        {
            foreach (var id in timeSheetEntryIds)
            {
                var timeSheetEntry = await _timeSheetEntryRepository.GetAsync(id);
                if (timeSheetEntry.LeavePermissionId.HasValue)
                {
                    await _leavePermissionRepository.DeleteAsync(timeSheetEntry.LeavePermissionId.Value);
                }
                await _timeSheetEntryRepository.DeleteAsync(id);
            }
        }

        public async Task<List<TimeSheetSubmitterCalendarDto>> GetTimeSheetSubmitterInformation(GetTimeSheetSubmitterInformationRequest request)
        {
            var timeSheetSubmitterInformation =  await _timeSheetEntryRepository.GetTimeSheetSubmitterInformationForApprover(request);
            var currentResource = await _resourceAppService.GetResourceByUserIdAsync(request.UserId);
            if (!timeSheetSubmitterInformation.Select(x => x.ResourceId == currentResource.Id).Any())
            {
                timeSheetSubmitterInformation.Add(new GetTimeSheetSubmitterInformationResponseDto()
                {
                    ResourceId = currentResource.Id,
                    ResourceName = $"{currentResource.FirstName} {currentResource.LastName}",
                    ApprovalStatus = null,
                });
            }

            var groupedTimeSheetListByUser = timeSheetSubmitterInformation.OrderBy(x => x.ResourceId.CompareTo(currentResource.Id))
                .GroupBy(u => u.ResourceId)
                .Select(grp => grp.ToList())
                .ToList();

            var result = new List<TimeSheetSubmitterCalendarDto>();
            
            foreach (var timeSheetGroupByUser in groupedTimeSheetListByUser)
            {
                // Group by days
                var groupedTimeSheetListByDays = timeSheetGroupByUser.GroupBy(u => u.RecordDate)
                    .Select(grp => grp.ToList()).ToList();

                var calendarItemSubmitTimeSheets = groupedTimeSheetListByDays.Select(timeSheet => new CalendarItemSubmitTimeSheetDto 
                {
                    Hours = timeSheet.Select(x => x.Hours).Sum(), 
                    Date = timeSheet.Select(x => x.RecordDate).FirstOrDefault(), 
                    ApproveStatus = CalculateApprovalStatusOfTimeSheetCalendarElement(timeSheet.Select(x => new CalendarStatusDto()
                    {
                        ApprovalStatus = x.ApprovalStatus,
                        Hours = x.Hours
                    }).ToList())
                }).ToList();

                var representTimeSheetGroupByUser = timeSheetGroupByUser.FirstOrDefault();

                if (representTimeSheetGroupByUser != null)
                {
                    var timeSheetCalendarElement = new TimeSheetSubmitterCalendarDto()
                    {
                        ResourceId = representTimeSheetGroupByUser.ResourceId,
                        ResourceName = representTimeSheetGroupByUser.ResourceName,
                        TotalHours = timeSheetGroupByUser.Select(x => x.Hours).Sum(),
                        CalendarItemSubmitTimeSheets = calendarItemSubmitTimeSheets
                    };

                    result.Add(timeSheetCalendarElement);
                }
            }

            return result;
        }

        public async Task<List<TimeSheetSubmitterCalendarDto>> GetTimeSheetSubmitterInformationForCurrentUser(GetTimeSheetSubmitterInformationRequest request)
        {
            var timeSheetSubmitterInformation = await _timeSheetEntryRepository.GetTimeSheetCalendarForCurrentUser(request);
            var groupedTimeSheetListByUser = timeSheetSubmitterInformation
                .GroupBy(u => u.ResourceId)
                .Select(grp => grp.ToList())
                .ToList();

            var result = new List<TimeSheetSubmitterCalendarDto>();

            foreach (var timeSheetGroupByUser in groupedTimeSheetListByUser)
            {
                // Group by days
                var groupedTimeSheetListByDays = timeSheetGroupByUser.GroupBy(u => u.RecordDate)
                    .Select(grp => grp.ToList()).ToList();

                var calendarItemSubmitTimeSheets = groupedTimeSheetListByDays.Select(timeSheet => new CalendarItemSubmitTimeSheetDto
                {
                    Hours = timeSheet.Select(x => x.Hours).Sum(),
                    Date = timeSheet.Select(x => x.RecordDate).FirstOrDefault(),
                    ApproveStatus = CalculateApprovalStatusOfTimeSheetCalendarElement(timeSheet.Select(x => new CalendarStatusDto()
                    {
                        ApprovalStatus = x.ApprovalStatus,
                        Hours = x.Hours 
                    }).ToList())
                }).ToList();

                var representTimeSheetGroupByUser = timeSheetGroupByUser.FirstOrDefault();

                if (representTimeSheetGroupByUser != null)
                {
                    var timeSheetCalendarElement = new TimeSheetSubmitterCalendarDto()
                    {
                        ResourceId = representTimeSheetGroupByUser.ResourceId,
                        ResourceName = representTimeSheetGroupByUser.ResourceName,
                        TotalHours = timeSheetGroupByUser.Select(x => x.Hours).Sum(),
                        CalendarItemSubmitTimeSheets = calendarItemSubmitTimeSheets
                    };

                    result.Add(timeSheetCalendarElement);
                }
            }

            return result;
        }

        public async Task<List<SubmitTimesheetEmailDto>> GetSubmittedTimeSheetEmailInformation(List<Guid> timeSheetEntryIds, string userFullName)
        {
                var submitTimeSheetEmailsResult = new List<SubmitTimesheetEmailDto>();
                var listIds = new DataTable("dbo.TimeSheetIDList");
                listIds.Columns.Add("ID", typeof(Guid));
                listIds.Columns.Add("RowNumber", typeof(int));
                var i = 1;
                foreach (var id in timeSheetEntryIds)
                {
                    var workRow = listIds.NewRow();
                    workRow["ID"] = id;
                    workRow["RowNumber"] = i;
                    listIds.Rows.Add(workRow);
                    i++;
                }

                var approvalInformationRequest = new ApprovalInformationRequestDto()
                {
                    TimesheetEntryIds = listIds
                };

                var approvalInformationResult = await _timeSheetEntryRepository.GetApprovalInformation(approvalInformationRequest);
                if (approvalInformationResult == null || !approvalInformationResult.Any())
                {
                    throw new ApprovalInformationNotFoundException($"Cannot find approval information for timeSheetEntryIds: {timeSheetEntryIds}");
                }

                var dates = approvalInformationResult.Select(x => x.RecordDate).ToList();

                var representDate = dates.FirstOrDefault();
                var startDate = representDate.StartOfWeek(DayOfWeek.Monday).ToString("yyyy-M-d");
                var endDate = representDate.StartOfWeek(DayOfWeek.Monday).AddDays(6).ToString("yyyy-M-d");

                foreach (var approvalInformation in approvalInformationResult)
                {
                    if (approvalInformation != null)
                    {
                        var submittedTimeSheetEmailElement = new SubmitTimesheetEmailDto()
                        {
                            ApprovalEmail = approvalInformation.ApprovalEmail,
                            ApprovalFullName = approvalInformation.ApprovalFullName,
                            ApproveTimeSheetLink = BuildTimeSheetLinkForSubmit(approvalInformation, startDate, endDate),
                            EmailSupport = "cuc.nguyen@Provide it here.se",
                            SkypeSupport = "cuc.nguyen",
                            SubmitterFullName = userFullName
                        };

                        if (submitTimeSheetEmailsResult.Exists(x => x.ApprovalEmail.Equals(submittedTimeSheetEmailElement.ApprovalEmail,
                            StringComparison.InvariantCultureIgnoreCase))) continue;

                        submitTimeSheetEmailsResult.Add(submittedTimeSheetEmailElement);
                    }
                }
                
                return submitTimeSheetEmailsResult;
        }

        public async Task<ApproveOrRejectTimeSheetEmailDto> GetApprovedOrRejectedTimeSheetEmailInformation(List<Guid> timeSheetEntryIds, string approvalFullName, string comment = null)
        {
            var listIds = new DataTable("dbo.TimeSheetIDList");
            listIds.Columns.Add("ID", typeof(Guid));
            listIds.Columns.Add("RowNumber", typeof(int));
            var i = 1;
            foreach (var id in timeSheetEntryIds)
            {
                var workRow = listIds.NewRow();
                workRow["ID"] = id;
                workRow["RowNumber"] = i;
                listIds.Rows.Add(workRow);
                i++;
            }

            var request = new GetApprovedOrRejectedTimeSheetInformationRequestDto()
            {
                TimeSheetEntryIds = listIds
            };

            var submitterInformationResults = await _timeSheetEntryRepository.GetApprovedOrRejectedTimeSheetInformation(request);
            
            if (submitterInformationResults == null || !submitterInformationResults.Any())
            {
                throw new SubmitterInformationNotFoundException("Cannot find submitter information");
            }

            var submitterInformationDuplicateList = submitterInformationResults.GroupBy(x => x.SubmitterEmail).Where(x => x.Skip(1).Any());
            
            if (submitterInformationDuplicateList.Count() > 1)
            {
                throw new HasMoreThanOneSubmitterException("These time sheet entries has more than one submitters");
            }

            var submitterInformation = submitterInformationResults.FirstOrDefault();

            if (submitterInformation == null)
            {
                throw new SubmitterInformationNotFoundException("Cannot find submitter information");
            }

            var linkTimeSheet = BuildTimeSheetLinkForApproveOrReject(submitterInformationResults);

            return new ApproveOrRejectTimeSheetEmailDto
            {
                SubmitterEmail = submitterInformation.SubmitterEmail,
                SubmitterFullName = submitterInformation.SubmitterFullName,
                ApprovalFullName = approvalFullName,
                TimeSheetLink = linkTimeSheet,
                Comment = comment,
                EmailSupport = "cuc.nguyen@Provide it here.se",
                SkypeSupport = "cuc.nguyen"
            };
        }

        public async Task<bool> CanUserApproveOrRejectTheseTimeSheets(IEnumerable<Guid> timeSheetEntryIds, long userId)
        {
            var listIds = new DataTable("dbo.TimeSheetIDList");
            listIds.Columns.Add("ID", typeof(Guid));
            listIds.Columns.Add("RowNumber", typeof(int));
            var i = 1;
            foreach (var id in timeSheetEntryIds)
            {
                var workRow = listIds.NewRow();
                workRow["ID"] = id;
                workRow["RowNumber"] = i;
                listIds.Rows.Add(workRow);
                i++;
            }

            var canUserApproveOrRejectTimeSheetRequest = new CanUserApproveOrRejectThisTimeSheetRequestDto()
            {
                TimeSheetEntryIds = listIds,
                UserId = userId
            };

            var result = await _timeSheetEntryRepository.CheckWhetherThisUserCanApproveOrRejectTimeSheet(
                canUserApproveOrRejectTimeSheetRequest);

            if (result == null || !result.Any())
            {
                throw new UserFriendlyException("Cannot check approve or reject time sheet information");
            }

            var canUserApproveOrRejectTimeSheet = result.FirstOrDefault();
            if (canUserApproveOrRejectTimeSheet == null)
            {
                throw new UserFriendlyException("Cannot check approve or reject time sheet information");
            }

            return canUserApproveOrRejectTimeSheet.Result;
        }

        public async Task<bool> CanUserSaveOrSubmitOrDeleteTheseTimeSheets(IEnumerable<Guid> timeSheetEntryIds, long userId)
        {
            var listIds = new DataTable("dbo.TimeSheetIDList");
            listIds.Columns.Add("ID", typeof(Guid));
            listIds.Columns.Add("RowNumber", typeof(int));
            var i = 1;
            foreach (var id in timeSheetEntryIds)
            {
                var workRow = listIds.NewRow();
                workRow["ID"] = id;
                workRow["RowNumber"] = i;
                listIds.Rows.Add(workRow);
                i++;
            }

            var canUserSaveOrSubmitTimeSheetRequestDto = new CanUserSaveOrSubmitTimeSheetRequestDto()
            {
                TimeSheetEntryIds = listIds,
                UserId = userId
            };

            var result = await _timeSheetEntryRepository.CheckWhetherThisUserCanSaveOrSubmitTimeSheet(
                canUserSaveOrSubmitTimeSheetRequestDto);

            if (result == null || !result.Any())
            {
                throw new UserFriendlyException("Cannot check save or submit time sheet information");
            }

            var canUserApproveOrRejectTimeSheet = result.FirstOrDefault();
            if (canUserApproveOrRejectTimeSheet == null)
            {
                throw new UserFriendlyException("Cannot check save or submit time sheet information");
            }

            return canUserApproveOrRejectTimeSheet.Result;
        }

        public async Task<bool> CheckWhetherTheseTimeSheetsContainSubmittedOrApprovedTimeSheet(IEnumerable<Guid> timeSheetEntryIds)
        {
            var listIds = new DataTable("dbo.TimeSheetIDList");
            listIds.Columns.Add("ID", typeof(Guid));
            listIds.Columns.Add("RowNumber", typeof(int));
            var i = 1;
            foreach (var id in timeSheetEntryIds)
            {
                var workRow = listIds.NewRow();
                workRow["ID"] = id;
                workRow["RowNumber"] = i;
                listIds.Rows.Add(workRow);
                i++;
            }

            var canUserSaveOrSubmitTimeSheetRequestDto = new TimeSheetsRequestDto()
            {
                TimeSheetEntryIds = listIds,
            };

            var result = await _timeSheetEntryRepository.CheckWhetherTheseTimeSheetsContainSubmittedOrApprovedTimeSheets(canUserSaveOrSubmitTimeSheetRequestDto);

            if (result == null || !result.Any())
            {
                throw new UserFriendlyException("Cannot find information");
            }

            var contained = result.FirstOrDefault();
            if (contained == null)
            {
                throw new UserFriendlyException("Cannot find information when checking whether time sheets contain submitted or approved time sheet");
            }

            return contained.Result;
        }

        public async Task<bool> CheckWhetherTheseTimeSheetsContainApprovedTimeSheet(IEnumerable<Guid> timeSheetEntryIds)
        {
            var listIds = new DataTable("dbo.TimeSheetIDList");
            listIds.Columns.Add("ID", typeof(Guid));
            listIds.Columns.Add("RowNumber", typeof(int));
            var i = 1;
            foreach (var id in timeSheetEntryIds)
            {
                var workRow = listIds.NewRow();
                workRow["ID"] = id;
                workRow["RowNumber"] = i;
                listIds.Rows.Add(workRow);
                i++;
            }

            var canUserSaveOrSubmitTimeSheetRequestDto = new TimeSheetsRequestDto()
            {
                TimeSheetEntryIds = listIds,
            };

            var result = await _timeSheetEntryRepository.CheckWhetherTheseTimeSheetsContainApprovedTimeSheets(canUserSaveOrSubmitTimeSheetRequestDto);

            if (result == null || !result.Any())
            {
                throw new UserFriendlyException("Cannot find information");
            }

            var contained = result.FirstOrDefault();
            if (contained == null)
            {
                throw new UserFriendlyException("Cannot find information when checking whether time sheets contain approved time sheet");
            }

            return contained.Result;
        }

        public async Task<bool> CheckWhetherTheseTimeSheetsContainRejectedTimeSheet(IEnumerable<Guid> timeSheetEntryIds)
        {
            var listIds = new DataTable("dbo.TimeSheetIDList");
            listIds.Columns.Add("ID", typeof(Guid));
            listIds.Columns.Add("RowNumber", typeof(int));
            var i = 1;
            foreach (var id in timeSheetEntryIds)
            {
                var workRow = listIds.NewRow();
                workRow["ID"] = id;
                workRow["RowNumber"] = i;
                listIds.Rows.Add(workRow);
                i++;
            }

            var canUserSaveOrSubmitTimeSheetRequestDto = new TimeSheetsRequestDto()
            {
                TimeSheetEntryIds = listIds,
            };

            var result = await _timeSheetEntryRepository.CheckWhetherTheseTimeSheetsContainRejectedTimeSheets(canUserSaveOrSubmitTimeSheetRequestDto);

            if (result == null || !result.Any())
            {
                throw new UserFriendlyException("Cannot find information");
            }

            var contained = result.FirstOrDefault();
            if (contained == null)
            {
                throw new UserFriendlyException("Cannot find information when checking whether time sheets contain approved time sheet");
            }

            return contained.Result;
        }

        public List<TimeSheetEntryResponse> ToTimeSheetEntryResponse(IEnumerable<TimeSheetEntryFromAPeriodResponse> timeSheetEntryFromPeriod)
        {
            var result = new List<TimeSheetEntryResponse>();
            timeSheetEntryFromPeriod = timeSheetEntryFromPeriod.ToList();
            var timeSheetsGroupedByTaskId = timeSheetEntryFromPeriod.Where(x => x.TaskId.HasValue && !x.LeaveTypeId.HasValue)
                .GroupBy(x => x.TaskId).Select(grp => grp.ToList()).ToList();

            var timeSheetTypeLeaveGroupedByLeaveTypeId = timeSheetEntryFromPeriod.Where(x => x.LeaveTypeId.HasValue && !x.TaskId.HasValue)
                .GroupBy(x => x.LeaveTypeId).Select(grp => grp.ToList()).ToList();
            foreach (var timeSheets in timeSheetsGroupedByTaskId)
            {
                var timeSheetEntriesInDay = timeSheets.Select(timeSheet => new TimeSheetEntryInDay
                {
                    TimeSheetEntryId = timeSheet.TimeSheetEntryId,
                    ApprovalStatus = timeSheet.ApprovalStatus != null
                        ? ((ApproveStatus) timeSheet.ApprovalStatus.Value).ToString()
                        : ETConsts.SavedTimeSheetStatus,
                    IsOverTime = timeSheet.IsOverTime,
                    RecordDate = timeSheet.RecordDate,
                    Hours = timeSheet.Hours
                }).ToList();

                var representTimeSheetEntryInDay = timeSheets.FirstOrDefault();
                if (representTimeSheetEntryInDay == null) continue;

                var timeSheetEntryResponse = new TimeSheetEntryResponse()
                {
                    TimeSheetEntriesInDay = timeSheetEntriesInDay,
                    AllocationId = representTimeSheetEntryInDay.AllocationId,
                    ApprovalId = representTimeSheetEntryInDay.ApprovalId,
                    Description = representTimeSheetEntryInDay.Description,
                    LeavePermissionId = representTimeSheetEntryInDay.LeavePermissionId,
                    LeaveTypeId = representTimeSheetEntryInDay.LeaveTypeId,
                    LeaveTypeName = representTimeSheetEntryInDay.LeaveTypeName,
                    SowCode = representTimeSheetEntryInDay.SowCode,
                    TaskInformation = representTimeSheetEntryInDay.TaskId.HasValue ? new TaskDto()
                    {
                        Id = representTimeSheetEntryInDay.TaskId.Value,
                        Name = representTimeSheetEntryInDay.TaskName,
                        TaskCategoryId = representTimeSheetEntryInDay.TaskCategoryId
                    } : null,
                    TaskCategory = representTimeSheetEntryInDay.TaskCategoryId.HasValue ? new TaskCategoryDto()
                    {
                        Id = representTimeSheetEntryInDay.TaskCategoryId.Value,
                        Name = representTimeSheetEntryInDay.TaskCategoryName
                    } : null,
                    TicketName = representTimeSheetEntryInDay.TicketName
                };
                
                result.Add(timeSheetEntryResponse);
            }


            foreach (var timeSheets in timeSheetTypeLeaveGroupedByLeaveTypeId)
            {
                var timeSheetEntriesInDay = timeSheets.Select(timeSheet => new TimeSheetEntryInDay
                {
                    TimeSheetEntryId = timeSheet.TimeSheetEntryId,
                    ApprovalStatus = timeSheet.ApprovalStatus != null
                        ? ((ApproveStatus)timeSheet.ApprovalStatus.Value).ToString()
                        : ETConsts.SavedTimeSheetStatus,
                    IsOverTime = timeSheet.IsOverTime,
                    RecordDate = timeSheet.RecordDate,
                    Hours = timeSheet.Hours
                }).ToList();

                var representTimeSheetEntryInDay = timeSheets.FirstOrDefault();
                if (representTimeSheetEntryInDay == null) continue;

                var timeSheetEntryResponse = new TimeSheetEntryResponse()
                {
                    TimeSheetEntriesInDay = timeSheetEntriesInDay,
                    AllocationId = representTimeSheetEntryInDay.AllocationId,
                    ApprovalId = representTimeSheetEntryInDay.ApprovalId,
                    Description = representTimeSheetEntryInDay.Description,
                    LeavePermissionId = representTimeSheetEntryInDay.LeavePermissionId,
                    LeaveTypeId = representTimeSheetEntryInDay.LeaveTypeId,
                    LeaveTypeName = representTimeSheetEntryInDay.LeaveTypeName,
                    SowCode = representTimeSheetEntryInDay.SowCode,
                    TaskInformation = representTimeSheetEntryInDay.TaskId.HasValue ? new TaskDto()
                    {
                        Id = representTimeSheetEntryInDay.TaskId.Value,
                        Name = representTimeSheetEntryInDay.TaskName,
                        TaskCategoryId = representTimeSheetEntryInDay.TaskCategoryId
                    } : null,
                    TaskCategory = representTimeSheetEntryInDay.TaskCategoryId.HasValue ? new TaskCategoryDto()
                    {
                        Id = representTimeSheetEntryInDay.TaskCategoryId.Value,
                        Name = representTimeSheetEntryInDay.TaskCategoryName
                    } : null,
                    TicketName = representTimeSheetEntryInDay.TicketName
                };

                result.Add(timeSheetEntryResponse);
            }
            return result;
        }

        public List<CreateOrUpdateTimeSheetEntryDto> ToCreateOrUpdateTimeSheetEntryRequest(IEnumerable<CreateOrUpdateTimeSheetEntryRequestDto> timeSheetEntryFromPeriod)
        {
            var result = new List<CreateOrUpdateTimeSheetEntryDto>();
            foreach (var timeSheetEntries in timeSheetEntryFromPeriod)
            {
                result.AddRange(timeSheetEntries.TimeSheetEntriesInDay.Select(timeSheet => new CreateOrUpdateTimeSheetEntryDto()
                {
                    AllocationId = timeSheetEntries.AllocationId,
                    ApprovalId = timeSheetEntries.ApprovalId,
                    Description = timeSheetEntries.Description,
                    LeavePermissionId = timeSheetEntries.LeavePermissionId,
                    IsLeaveType = timeSheetEntries.IsLeaveType,
                    TaskId = timeSheetEntries.TaskInFormation?.Id,
                    LeaveTypeId = timeSheetEntries.LeaveTypeId,
                    TicketName = timeSheetEntries.TicketName,
                    TimeSheetEntryId = timeSheet.TimeSheetEntryId,
                    Hours = timeSheet.Hours,
                    IsOverTime = timeSheet.IsOverTime,
                    RecordDate = timeSheet.RecordDate
                }));
            }

            return result;
        }

        private string BuildTimeSheetLinkForApproveOrReject(IEnumerable<SubmitterTimeSheetInformationDto> submitterInformationResult)
        {
            var submitterResult = submitterInformationResult.ToList();
            var dates = submitterResult.Select(x => x.RecordDate).ToList();
            var representDate = dates.FirstOrDefault();
            var startDate = representDate.StartOfWeek(DayOfWeek.Monday).ToString("yyyy-M-d");
            var endDate = representDate.StartOfWeek(DayOfWeek.Monday).AddDays(6).ToString("yyyy-M-d");
            return _appConfiguration["App:ClientRootAddress"].EnsureEndsWith('/') +
                   "time-entry/timesheet-list" + $"?startDate={startDate}&endDate={endDate}";
        }

        private string BuildTimeSheetLinkForSubmit(ApprovalInformationResponseDto approvalInformationResult, string startDate, string endDate)
        {
            var resourceId = approvalInformationResult.SubmitterResourceId;
            return _appConfiguration["App:ClientRootAddress"].EnsureEndsWith('/') +
                   "time-entry/timesheet-approval" + $"?resourceId={resourceId}&startDate={startDate}&endDate={endDate}";
        }

        private static string CalculateApprovalStatusOfTimeSheetCalendarElement(List<CalendarStatusDto> approvalStatuses)
        {
            if (approvalStatuses == null || !approvalStatuses.Any())
            {
                throw new ArgumentNullException(nameof(approvalStatuses));
            }
            

            if (approvalStatuses.Any(x => !x.ApprovalStatus.HasValue) && approvalStatuses.Count == 1 && 
                approvalStatuses.FirstOrDefault()?.Hours == null)
            {
                return null;
            }

            if (approvalStatuses.Any(x => !x.ApprovalStatus.HasValue))
            {
                return ETConsts.SavedTimeSheetStatus;
            }

            if (approvalStatuses.FirstOrDefault(x => x.ApprovalStatus == (byte?)ApproveStatus.Rejected) != null)
            {
                return ApproveStatus.Rejected.ToString();
            }

            if (approvalStatuses.FirstOrDefault(x => x.ApprovalStatus == (byte?)ApproveStatus.WaitingForApproval) != null)
            {
                return ApproveStatus.WaitingForApproval.ToString();
            }

            return ApproveStatus.Approved.ToString();
        }
    }
}
