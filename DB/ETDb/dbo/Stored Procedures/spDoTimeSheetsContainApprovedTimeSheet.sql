
CREATE PROCEDURE [dbo].[spDoTimeSheetsContainApprovedTimeSheet]
	 @TimeSheetEntryIds AS dbo.TimeSheetIDList READONLY
AS
	DECLARE @Result bit = 'false'
	DECLARE @cnt INT = 1;
	DECLARE @MAX INT = (SELECT COUNT(*) FROM @TimeSheetEntryIds)
	WHILE @cnt <= @MAX
		BEGIN
			DECLARE @status bit = (SELECT timeSheetEntry.ApprovalStatus 
			FROM dbo.TimesheetEntry timeSheetEntry
			WHERE timeSheetEntry.Id = (SELECT [Id] FROM @TimeSheetEntryIds WHERE RowNumber = @cnt))
			IF (@status = 1)
			BEGIN
				SET @Result = 'true'
			END
			SET @cnt = @cnt + 1;
		END
	SELECT @Result AS Result