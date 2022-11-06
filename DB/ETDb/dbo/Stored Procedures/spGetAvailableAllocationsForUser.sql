
CREATE PROCEDURE [dbo].[spGetAvailableAllocationsForUser]
	@UserName nvarchar(256) NULL,
	@UserId bigint null
AS
SELECT 
allocation.Id AS AllocationId,
MAX(CONCAT(project.ProjectTag,'.',sow.SowNumber,'.',sow.Version,'.',sowRole.RoleName,'.',allocation.StartDate,'.',allocation.EndDate)) AS SowCode,
MAX(project.Name) AS ProjectName,
MAX(allocation.TimeNote) AS TimeNote,
MAX(sowRole.Id) AS SowRoleId,
MAX(project.ProjectManagerId) AS ProjectManagerId,
allocation.StartDate AS StartDate,
allocation.EndDate AS EndDate,
allocation.TotalHours AS TotalHours,
allocation.TotalHoursPerMonth AS TotalHoursPerMonth,
allocation.FTE AS FTE,
sowRole.RoleName
FROM dbo.SOWRole sowRole
JOIN
dbo.SOW sow
ON sow.Id = sowRole.SOWId
JOIN
dbo.Project project
ON project.Id = sow.ProjectId
JOIN
dbo.Allocation allocation
ON sowRole.Id = allocation.SOWRoleId
JOIN
dbo.Resource resource
ON
allocation.ResourceId = resource.Id
JOIN
dbo.AbpUsers users
ON 
resource.UserId = users.Id
WHERE users.UserName = @UserName AND users.Id = @UserId
GROUP BY allocation.Id, allocation.StartDate, allocation.EndDate, allocation.TotalHours, allocation.TotalHoursPerMonth, allocation.FTE, sowRole.RoleName