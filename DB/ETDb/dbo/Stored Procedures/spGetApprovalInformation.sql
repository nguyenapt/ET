

CREATE PROCEDURE [dbo].[spGetApprovalInformation] 
	@TimeSheetEntryIds AS dbo.TimeSheetIDList READONLY
AS
	create table #tmp4 (
	 TimeSheetEntryId UNIQUEIDENTIFIER,
	 ApprovalFullName NVARCHAR(50),
	 ApprovalEmail NVARCHAR(256),
	 SubmitterFullName NVARCHAR(50),
	 RecordDate DATETIME,
	 SubmitterResourceId UNIQUEIDENTIFIER 
	)

	SELECT 
	CONCAT(resource.FirstName,' ', resource.LastName) AS ApprovalFullName,
	users.EmailAddress AS ApprovalEmail,
	timeSheetEntry.Id AS TimeSheetEntryId
	into #tmp1
	FROM dbo.Resource resource
	JOIN
	dbo.AbpUsers users
	ON resource.UserName = users.UserName 
	JOIN
	dbo.TimesheetEntry timeSheetEntry
	ON
	timeSheetEntry.ApprovalId = resource.Id
	WHERE
	timeSheetEntry.Id IN (SELECT [Id] FROM @TimeSheetEntryIds)

	SELECT
	timeSheetEntry.RecordDate,
	timeSheetEntry.Id AS TimeSheetEntryId,
	CONCAT(resources.FirstName,' ', resources.LastName) AS SubmitterFullName,
	resources.Id AS SubmitterResourceId
	INTO #tmp2
	FROM dbo.Resource resources
	JOIN dbo.Allocation allocation
	ON allocation.ResourceId = resources.Id
	JOIN dbo.TimesheetEntry timeSheetEntry
	ON timeSheetEntry.AllocationId = allocation.Id
	WHERE
	timeSheetEntry.Id IN (SELECT [Id] FROM @TimeSheetEntryIds)

	SELECT
	timeSheetEntry.RecordDate,
	timeSheetEntry.Id AS TimeSheetEntryId,
	CONCAT(resources.FirstName,' ', resources.LastName) AS SubmitterFullName,
	resources.Id AS SubmitterResourceId
	INTO #tmp3
	FROM dbo.Resource resources
	JOIN dbo.LeavePermission leavePermission
	ON leavePermission.ResourceId = resources.Id
	JOIN dbo.TimeSheetEntry timeSheetEntry
	ON timeSheetEntry.LeavePermissionId = leavePermission.Id
	WHERE
	timeSheetEntry.Id IN (SELECT [Id] FROM @TimeSheetEntryIds)

	SELECT
	#tmp2.TimeSheetEntryId,
	#tmp1.ApprovalFullName AS ApprovalFullName,
	#tmp1.ApprovalEmail  AS ApprovalEmail,
	#tmp2.SubmitterFullName,
	#tmp2.RecordDate,
	#tmp2.SubmitterResourceId 
	INTO #tmpa
	FROM  #tmp1
	JOIN #tmp2
	ON #tmp1.TimeSheetEntryId = #tmp2.TimeSheetEntryId

	SELECT
	#tmp3.TimeSheetEntryId,
	#tmp1.ApprovalFullName AS ApprovalFullName,
	#tmp1.ApprovalEmail  AS ApprovalEmail,
	#tmp3.SubmitterFullName,
	#tmp3.RecordDate,
	#tmp3.SubmitterResourceId 
	INTO #tmpb
	FROM  #tmp1
	JOIN #tmp3
	ON #tmp1.TimeSheetEntryId = #tmp3.TimeSheetEntryId

	insert into #tmp4 (TimeSheetEntryId,
	 ApprovalFullName,
	 ApprovalEmail,
	 SubmitterFullName,
	 RecordDate,
	 SubmitterResourceId) 
	select TimeSheetEntryId, ApprovalFullName, ApprovalEmail, SubmitterFullName, RecordDate, SubmitterResourceId from #tmpa

	insert into #tmp4 (TimeSheetEntryId,
	 ApprovalFullName,
	 ApprovalEmail,
	 SubmitterFullName,
	 RecordDate,
	 SubmitterResourceId) 
	select TimeSheetEntryId, ApprovalFullName, ApprovalEmail, SubmitterFullName, RecordDate, SubmitterResourceId from #tmpb

	select * from #tmp4

	DROP TABLE #tmp1
	DROP TABLE #tmp2
	DROP TABLE #tmp3
	DROP TABLE #tmpa
	DROP TABLE #tmpb
	DROP TABLE #tmp4