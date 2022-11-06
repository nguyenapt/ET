

CREATE PROCEDURE [dbo].[spGetSowRolesWithSupporterType] 
	@SowId uniqueidentifier
AS
SELECT sowRole.* FROM dbo.SOWRole sowRole JOIN dbo.InternalType internalType
on sowRole.InternalTypeId = internalType.Id
WHERE sowRole.SOWId = @SowId
AND sowRole.IsDeleted = 'false'
AND internalType.Name = 'Internal'