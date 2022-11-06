
CREATE PROCEDURE [dbo].[spGetSubmitterInformationOfTimeSheet] 
	@TimeSheetEntryIds AS dbo.TimeSheetIDList READONLY
AS
	create table #tmp (
		SubmitterEmail NVARCHAR(250),
		SubmitterFullName NVARCHAR(50),
		RecordDate DATETIME,
	)

	SELECT users.EmailAddress AS SubmitterEmail, 
	CONCAT(resources.FirstName,' ', resources.LastName) AS SubmitterFullName,
	timeSheetEntry.RecordDate as RecordDate
	INTO #tmp1
	FROM dbo.TimesheetEntry timeSheetEntry
	JOIN dbo.Allocation allocation
	ON allocation.Id = timeSheetEntry.AllocationId
	JOIN dbo.Resource resources
	ON resources.Id = allocation.ResourceId
	JOIN dbo.AbpUsers users
	ON users.Id = resources.UserId
	WHERE timeSheetEntry.Id IN (SELECT [Id] FROM @TimeSheetEntryIds)

	SELECT users.EmailAddress AS SubmitterEmail, 
	CONCAT(resources.FirstName,' ', resources.LastName) AS SubmitterFullName,
	timeSheetEntry.RecordDate as RecordDate
	INTO #tmp2
	FROM dbo.TimesheetEntry timeSheetEntry
	JOIN dbo.LeavePermission leavePermission
	ON leavePermission.Id = timeSheetEntry.LeavePermissionId
	JOIN dbo.Resource resources
	ON resources.Id = leavePermission.ResourceId
	JOIN dbo.AbpUsers users
	ON users.Id = resources.UserId
	WHERE timeSheetEntry.Id IN (SELECT [Id] FROM @TimeSheetEntryIds)

	insert into #tmp  (
	SubmitterEmail,
	SubmitterFullName,
	RecordDate) 
	select 
		SubmitterEmail,
		SubmitterFullName,
		RecordDate
	from #tmp1

	insert into #tmp  (
	SubmitterEmail,
	SubmitterFullName,
	RecordDate) 
	select 
		SubmitterEmail,
		SubmitterFullName,
		RecordDate
	from #tmp2

	select * from #tmp

DROP TABLE #tmp1
DROP TABLE #tmp2
DROP TABLE #tmp