CREATE PROCEDURE [dbo].[spGetNonSupporterSOWRoles]
	@sowId uniqueidentifier             
AS 
BEGIN   
SELECT sowRole.* FROM dbo.SOWRole sowRole
WHERE SowId = @sowId
AND NOT EXISTS (select top 1 * from dbo.InternalType internalType WHERE internalType.Id = sowRole.InternalTypeId)          
END