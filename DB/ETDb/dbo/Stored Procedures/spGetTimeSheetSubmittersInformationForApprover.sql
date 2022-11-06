﻿CREATE PROCEDURE [dbo].[spGetTimeSheetSubmittersInformationForApprover]
	 @UserId BIGINT,
	 @Year INT,
	 @Month INT
AS
BEGIN
	create table #tmp4 (
		ResourceId UNIQUEIDENTIFIER,
		ResourceName NVARCHAR(50),
		TimeSheetEntryId UNIQUEIDENTIFIER,
		AllocationId UNIQUEIDENTIFIER NULL,
		ApprovalStatus TINYINT NULL,
		Hours DECIMAL(18,2) NULL,
		RecordDate DATETIME NOT NULL,
		ApprovalId UNIQUEIDENTIFIER NULL
	)


	--- Get for submitters ---
	SELECT 
		timeSheetEntry.Id AS TimeSheetEntryId, 
		timeSheetEntry.AllocationId, 
		timeSheetEntry.Hours,
		timeSheetEntry.ApprovalStatus,
		timeSheetEntry.RecordDate,
		timeSheetEntry.ApprovalId,
		timeSheetEntry.LeavePermissionId
	INTO
		#tmp1
	FROM
		dbo.TimeSheetEntry timeSheetEntry
	JOIN	
		dbo.Resource resources
	ON
		resources.Id = timeSheetEntry.ApprovalId
	JOIN	
		dbo.AbpUsers users
	ON 
		users.UserName = resources.UserName
	WHERE
		users.Id = @UserId
	AND YEAR(timeSheetEntry.RecordDate) = @Year
	AND Month(timeSheetEntry.RecordDate) = @Month

	----Get time sheet for current user----
	SELECT 
		resources.Id as ResourceId,
		(CONCAT(resources.FirstName,' ',resources.LastName)) AS ResourceName,
		(timeSheetEntry.Id) AS TimeSheetEntryId,
		(timeSheetEntry.AllocationId) AS AllocationId,
		(timeSheetEntry.ApprovalStatus) AS ApprovalStatus,
		(timeSheetEntry.Hours) AS Hours,
		(timeSheetEntry.RecordDate) AS RecordDate,
		(timeSheetEntry.ApprovalId) AS ApprovalId
	INTO
		#tmp5
	FROM
		dbo.TimeSheetEntry timeSheetEntry
	JOIN	
		dbo.Allocation allocation
	ON
		allocation.Id = timeSheetEntry.AllocationId
	JOIN
		dbo.Resource resources
	ON
		resources.Id = allocation.ResourceId
	JOIN	
		dbo.AbpUsers users
	ON 
		users.UserName = resources.UserName
	WHERE
		users.Id = @UserId
	AND YEAR(timeSheetEntry.RecordDate) = @Year
	AND Month(timeSheetEntry.RecordDate) = @Month

	----Get time sheet for current user----
	SELECT 
	resources.Id as ResourceId,
	(CONCAT(resources.FirstName,' ',resources.LastName)) AS ResourceName,
	(timeSheetEntry.Id) AS TimeSheetEntryId,
	(timeSheetEntry.AllocationId) AS AllocationId,
	(timeSheetEntry.ApprovalStatus) AS ApprovalStatus,
	(timeSheetEntry.Hours) AS Hours,
	(timeSheetEntry.RecordDate) AS RecordDate,
	(timeSheetEntry.ApprovalId) AS ApprovalId
	INTO
		#tmp6
	FROM
		dbo.TimeSheetEntry timeSheetEntry
	JOIN	
		dbo.LeavePermission leavePermission
	ON
		leavePermission.Id = timeSheetEntry.LeavePermissionId
	JOIN
		dbo.Resource resources
	ON
		resources.Id = leavePermission.ResourceId
	JOIN	
		dbo.AbpUsers users
	ON 
		users.UserName = resources.UserName
	WHERE
		users.Id = @UserId
	AND YEAR(timeSheetEntry.RecordDate) = @Year
	AND Month(timeSheetEntry.RecordDate) = @Month

	SELECT
		resources.Id as ResourceId,
		(CONCAT(resources.FirstName,' ',resources.LastName)) AS ResourceName,
		(#tmp1.TimeSheetEntryId) AS TimeSheetEntryId,
		(#tmp1.AllocationId) AS AllocationId,
		(#tmp1.ApprovalStatus) AS ApprovalStatus,
		(#tmp1.Hours) AS Hours,
		(#tmp1.RecordDate) AS RecordDate,
		(#tmp1.ApprovalId) AS ApprovalId
	INTO
		#tmp2
	FROM	
		#tmp1
	JOIN	
		dbo.Allocation allocation
	ON	
		allocation.Id = #tmp1.AllocationId
	JOIN
		dbo.Resource resources
	ON
		resources.Id = allocation.ResourceId

	SELECT
		resources.Id as ResourceId,
		(CONCAT(resources.FirstName,' ',resources.LastName)) AS ResourceName,
		(#tmp1.TimeSheetEntryId) AS TimeSheetEntryId,
		(#tmp1.AllocationId) AS AllocationId,
		(#tmp1.ApprovalStatus) AS ApprovalStatus,
		(#tmp1.Hours) AS Hours,
		(#tmp1.RecordDate) AS RecordDate,
		(#tmp1.ApprovalId) AS ApprovalId
	INTO
		#tmp3
	FROM	
		#tmp1
	JOIN 
		dbo.LeavePermission leavePermission
	ON 
		leavePermission.Id = #tmp1.LeavePermissionId
	JOIN 
		dbo.Resource resources
	ON 
		resources.Id = leavePermission.ResourceId

	insert into #tmp4  (
	    ResourceId, 
		ResourceName, 
		TimeSheetEntryId, 
		AllocationId, 
		ApprovalStatus,
		Hours, 
		RecordDate, 
		ApprovalId
	) 
	select 
		ResourceId, 
		ResourceName, 
		TimeSheetEntryId, 
		AllocationId, 
	    ApprovalStatus, 
		Hours, 
		RecordDate, 
		ApprovalId 
	from #tmp2

	insert into #tmp4  (
	ResourceId, 
	ResourceName, 
	TimeSheetEntryId, 
	AllocationId, 
	ApprovalStatus,
	Hours, 
	RecordDate, 
	ApprovalId
	) 
	select 
		ResourceId, 
		ResourceName, 
		TimeSheetEntryId, 
		AllocationId, 
	    ApprovalStatus, 
		Hours, 
		RecordDate, 
		ApprovalId 
	from #tmp3

	insert into #tmp4  (
	ResourceId, 
	ResourceName, 
	TimeSheetEntryId, 
	AllocationId, 
	ApprovalStatus,
	Hours, 
	RecordDate, 
	ApprovalId
	) 
	select 
		ResourceId, 
		ResourceName, 
		TimeSheetEntryId, 
		AllocationId, 
	    ApprovalStatus, 
		Hours, 
		RecordDate, 
		ApprovalId 
	from #tmp5

	insert into #tmp4  (
	ResourceId, 
	ResourceName, 
	TimeSheetEntryId, 
	AllocationId, 
	ApprovalStatus,
	Hours, 
	RecordDate, 
	ApprovalId
	) 
	select 
		ResourceId, 
		ResourceName, 
		TimeSheetEntryId, 
		AllocationId, 
	    ApprovalStatus, 
		Hours, 
		RecordDate, 
		ApprovalId 
	from #tmp6

	select * from #tmp4


DROP TABLE #tmp1
DROP TABLE #tmp2
DROP TABLE #tmp3
DROP TABLE #tmp5
DROP TABLE #tmp6
DROP TABLE #tmp4
END