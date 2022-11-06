CREATE PROCEDURE [dbo].[spGetTimeSheetsForUserFromAPeriod] 
	 @StartDate DATE NULL,
	 @EndDate DATE NULL,
	 @ResourceId UNIQUEIDENTIFIER
AS
IF @StartDate <= @EndDate
BEGIN
	create table #tmp3 (
	TimeSheetEntryId UNIQUEIDENTIFIER,
	SowCode nvarchar(max),
	AllocationId UNIQUEIDENTIFIER NULL,
	Description nvarchar(250) null,
	Hours decimal(18,2) null,
	RecordDate DATETIME not null,
	ApprovalStatus tinyint null,
	TicketName nvarchar(20) null,
	ApprovalId UNIQUEIDENTIFIER null,
	LeavePermissionId UNIQUEIDENTIFIER NULL,
	LeaveTypeId UNIQUEIDENTIFIER NULL,
	LeaveTypeName nvarchar(256) NULL,
	TaskId UniqueIdentifier null,
	TaskName nvarchar(50) null,
	TaskCategoryId UNIQUEIDENTIFIER null,
	TaskCategoryName nvarchar(250) null
	)

	SELECT 
		timeSheetEntry.Id as TimeSheetEntryId,
		MAX(CONCAT(project.ProjectTag,'.',sow.SowNumber,'.',sow.Version)) AS SowCode,
		MAX(timeSheetEntry.AllocationId) as AllocationId,
		MAX(timeSheetEntry.Description) as Description,
		MAX(timeSheetEntry.Hours) as Hours,
		MAX(timeSheetEntry.RecordDate) as RecordDate,
		MAX(CONVERT(tinyint,ApprovalStatus)) as ApprovalStatus,
		MAX(CONVERT(tinyint,isOverTime)) as IsOverTime,
		MAX(timeSheetEntry.TicketName) as TicketName,
		MAX(timeSheetEntry.ApprovalId) as ApprovalId,
		MAX(timeSheetEntry.LeavePermissionId) as LeavePermissionId,
		MAX(task.Id) AS TaskId,
		MAX(task.Name) AS TaskName,
		MAX(taskCategory.Id) AS TaskCategoryId,
		MAX(taskCategory.Name) AS TaskCategoryName
	into #tmp1
	FROM dbo.TimesheetEntry timeSheetEntry
	JOIN dbo.Allocation allocation
	ON timeSheetEntry.AllocationId = allocation.Id
	JOIN dbo.SOWRole sowRole
	ON sowRole.Id = allocation.SOWRoleId
	JOIN dbo.SOW sow
	ON sow.Id = sowRole.SOWId
	JOIN dbo.Project project
	ON project.Id = sow.ProjectId
	JOIN dbo.Task task
	ON timeSheetEntry.TaskId = task.Id
	JOIN dbo.TaskCategory taskCategory
	ON task.TaskCategoryId = taskCategory.Id
	JOIN dbo.Resource resources
	ON allocation.ResourceId = resources.Id
	WHERE resources.Id = @ResourceId
	AND
	timeSheetEntry.RecordDate BETWEEN @StartDate AND @EndDate
	AND timeSheetEntry.LeavePermissionId is null
	GROUP BY timeSheetEntry.Id

	SELECT 
		timeSheetEntry.Id as TimeSheetEntryId,
		MAX(timeSheetEntry.Description) as Description,
		MAX(timeSheetEntry.Hours) as Hours,
		MAX(timeSheetEntry.RecordDate) as RecordDate,
		MAX(CONVERT(tinyint, timeSheetEntry.ApprovalStatus)) as ApprovalStatus,		
		MAX(timeSheetEntry.ApprovalId) as ApprovalId,
		MAX(timeSheetEntry.LeavePermissionId) as LeavePermissionId,
		MAX(leavePermission.LeaveTypeId) as LeaveTypeId,
		MAX(leaveType.Name) as LeaveTypeName
	into #tmp2
	FROM dbo.TimesheetEntry timeSheetEntry
	JOIN dbo.LeavePermission leavePermission
	ON leavePermission.Id = timeSheetEntry.LeavePermissionId
	JOIN dbo.LeaveType leaveType
	ON leaveType.Id = leavePermission.LeaveTypeId
	WHERE leavePermission.ResourceId = @ResourceId
	AND
	timeSheetEntry.RecordDate BETWEEN @StartDate AND @EndDate
	AND timeSheetEntry.TaskId is null 
	AND timeSheetEntry.AllocationId is null
	GROUP BY timeSheetEntry.Id

	insert into #tmp3 (TimeSheetEntryId, AllocationId, ApprovalId, ApprovalStatus, 
	Description, Hours, LeavePermissionId, RecordDate, SowCode, TaskCategoryId, TaskCategoryName, TaskId, TaskName, TicketName) 
	select TimeSheetEntryId, AllocationId, ApprovalId, ApprovalStatus, 
	Description, Hours, LeavePermissionId, RecordDate, SowCode, TaskCategoryId, TaskCategoryName, TaskId, TaskName, TicketName from #tmp1

	insert into #tmp3 (TimeSheetEntryId, Description, Hours, RecordDate, ApprovalStatus, ApprovalId, LeavePermissionId, LeaveTypeId, LeaveTypeName)
	select TimesheetEntryId, Description, Hours, RecordDate, ApprovalStatus, ApprovalId, LeavePermissionId, LeaveTypeId, LeaveTypeName from #tmp2

	select * from #tmp3
	
	DROP TABLE #tmp1
	DROP TABLE #tmp2
	DROP TABLE #tmp3
END
ELSE
	RAISERROR('End date should be equal or greater than Start date', 16,16)