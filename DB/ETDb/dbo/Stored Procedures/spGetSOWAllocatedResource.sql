CREATE PROCEDURE spGetSOWAllocatedResource  
 @departmentId uniqueidentifier,  
 @startDate datetime,        
 @endDate datetime  
AS        
BEGIN        
DECLARE @totalDays as int        
        
SET @totalDays = (        
case         
 when @startDate is null or @endDate is null then 1        
 when @startDate > @endDate then 1        
 else dbo.fn_WorkDays(@startDate, @endDate)        
end        
)        
   
SELECT   
  a.SOWId,   
  a.Id as SOWRoleId,   
  a.TotalHours,   
  a.TotalHoursPerMonth,  
  case when a.TotalHours=0 and a.TotalHoursPerMonth =0 then a.FTE else 0 end as FTEDemand,  
  
  0 as TotalHoursDemand,  
  0 as TotalHoursPerMonthDemand,  
    
  case when a.StartDate < @startDate then @startDate else a.StartDate end as FromDate,  
  case when a.EndDate is null or a.EndDate > @endDate then @endDate else a.EndDate end as ToDate,  
  a.StartDate,  
  a.EndDate  
  into #tmpSOWRole  
from dbo.SOWRole a join dbo.SOW b on a.SOWId = b.Id   
 join dbo.Project c on b.ProjectId = c.Id
where   
 (a.EndDate is null or a.EndDate >= @startDate)         
 and (a.StartDate <= @endDate) and b.Status in ('Open','Signed','Confirmed','Draft')    
 and (@departmentId is null or c.DepartmentId = @departmentId)  
      
--get needed time from @start to @endDate for each sowrole  
UPDATE #tmpSOWRole   
SET   
TotalHoursDemand = TotalHours * dbo.fn_WorkDays(FromDate, ToDate) /dbo.fn_WorkDays(StartDate,EndDate)   
where TotalHours is not null and EndDate is not null  
  
UPDATE #tmpSOWRole   
SET   
TotalHoursPerMonthDemand = TotalHoursPerMonth *  dbo.fn_WorkDays(FromDate, ToDate) /22  
where TotalHoursPerMonth is not null and ToDate is not null  
  
-- get allocation for all resource from @start to @enddate  
  
SELECT   
  a.SOWRoleId,   
  a.FTE,   
  a.TotalHours,  
  a.TotalHoursPerMonth,  
    
  case when a.StartDate < b.FromDate then b.FromDate else a.StartDate end as FromDate,    
  case when a.EndDate is null or a.EndDate > b.ToDate then b.ToDate else a.EndDate end as ToDate,  
  a.StartDate,  
  a.EndDate into #tmpAllocateResource  
  FROM dbo.Allocation a join #tmpSOWRole b on a.SOWRoleId = b.SOWRoleId   
where (a.EndDate is null or a.EndDate >=b.FromDate) and a.StartDate <= b.EndDate  
  
-- calculate allocation for each sowrole  
select   
 SOWRoleId,  
 sum(FTE) as FTEAllocate,  
 sum(TotalHours*dbo.fn_WorkDays(FromDate, ToDate)/dbo.fn_WorkDays(StartDate, EndDate)) as TotalHoursAllocate,  
 sum(TotalHoursPerMonth*dbo.fn_WorkDays(FromDate, ToDate)/22) as TotalHoursPerMonthAllocate  
 into #tmpAllocateSOWRole  
from #tmpAllocateResource group by SOWRoleId  
  
        
Select   
 a.SOWId,  
 a.SOWRoleId,  
 a.FTEDemand,  
 a.TotalHoursDemand,  
 a.TotalHoursPerMonthDemand,  
 b.FTEAllocate,  
 b.TotalHoursAllocate,  
 b.TotalHoursPerMonthAllocate  
FRom #tmpSOWRole a join #tmpAllocateSOWRole b on a.SOWRoleId = b.SOWRoleId  
--where Allocated is null or Allocated <= @fte        
        
DROP TABLE #tmpSOWRole        
DROP TABLE #tmpAllocateSOWRole        
DROP TABLE #tmpAllocateResource        
        
END