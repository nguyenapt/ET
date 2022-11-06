CREATE PROCEDURE [dbo].[spGetAllocationForResource]
	@ResourceId uniqueidentifier        
AS                  
BEGIN  
	SELECT 
	allocation.Id AS AllocationId,
	project.Name AS Project,
	department.Name AS Program,
	sowRole.RoleName AS Role,
	allocation.IsBillable AS IsBillable,
	allocation.StartDate AS StartDate,
	allocation.EndDate AS EndDate,
	allocation.IsActive AS IsActive
	FROM dbo.Allocation allocation
	JOIN
	dbo.SOWRole sowRole
	ON
	sowRole.Id = allocation.SOWRoleId
	JOIN
	dbo.SOW sow
	ON
	sow.Id = sowRole.SOWId
	JOIN
	dbo.Project project
	ON
	project.Id = sow.ProjectId
	JOIN
	dbo.Department department
	ON 
	department.Id = project.DepartmentId
	WHERE allocation.ResourceId = @resourceId 
	AND
	sow.Status in ('Open','Signed','Confirmed','Draft') 
END