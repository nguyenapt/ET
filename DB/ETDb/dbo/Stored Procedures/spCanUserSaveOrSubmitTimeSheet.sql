CREATE PROCEDURE [dbo].[spCanUserSaveOrSubmitTimeSheet]
	 @TimeSheetEntryIds AS dbo.TimeSheetIDList READONLY,
	 @UserId BIGINT
AS
	DECLARE @Result bit = 'true'
	DECLARE @cnt INT = 1;
	DECLARE @MAX INT = (SELECT COUNT(*) FROM @TimeSheetEntryIds)
	WHILE @cnt <= @MAX
		BEGIN
		    IF NOT EXISTS(
			SELECT * 
			FROM dbo.TimesheetEntry timeSheetEntry
			JOIN dbo.Allocation allocation
			ON allocation.Id = timeSheetEntry.AllocationId
			JOIN dbo.Resource resource
			ON resource.Id = allocation.ResourceId
			WHERE timeSheetEntry.Id = (SELECT [Id] FROM @TimeSheetEntryIds WHERE RowNumber = @cnt)
			AND resource.UserId = @UserId)
			AND NOT EXISTS (SELECT * 
			FROM dbo.TimesheetEntry timeSheetEntry
			JOIN dbo.LeavePermission leavePermission
			ON leavePermission.Id = timeSheetEntry.LeavePermissionId
			JOIN dbo.Resource resources
			ON resources.Id = leavePermission.ResourceId
			WHERE timeSheetEntry.Id = (SELECT [Id] FROM @TimeSheetEntryIds WHERE RowNumber = @cnt)
			AND resources.UserId = @UserId)
			BEGIN
				SET @Result = 'false'
			END
			SET @cnt = @cnt + 1;
		END;
	SELECT @Result AS Result;