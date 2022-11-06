CREATE PROCEDURE [dbo].[spCanUserApproveOrRejectTimeSheet]
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
			JOIN dbo.Resource resource
			ON resource.Id = timeSheetEntry.ApprovalId
			WHERE timeSheetEntry.Id = (SELECT [Id] FROM @TimeSheetEntryIds WHERE RowNumber = @cnt)
			AND resource.UserId = @UserId)
			BEGIN
				SET @Result = 'false'
			END
			SET @cnt = @cnt + 1;
		END;
	SELECT @Result AS Result