


CREATE PROCEDURE [dbo].[spGetAvailableResource]                      
 @departmentId uniqueidentifier,             
 @projectId uniqueidentifier,            
 @resourceName nvarchar(max),          
 @skillId uniqueidentifier,          
 @skillLevelId uniqueidentifier,          
 @startDate datetime,                      
 @endDate datetime,                      
 @fte float
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
select a.Id into #tmpResource          
 from dbo.Resource a      
 where (@departmentId is null or DepartmentId = @departmentId) and     
 (@resourceName is null or     
 LastName+' '+ FirstName like '%'+@resourceName+'%'      
 or FirstName+' '+ LastName like '%'+@resourceName+'%' 
 ) 
IF @skillId is not null          
begin          
 delete from #tmpResource where Id not in (select ResourceId from dbo.ResourceSkill where SkillId = @skillId)          
 if @skillLevelId is  not null          
 begin          
  delete from #tmpResource where Id not in (select ResourceId from dbo.ResourceSkill where SkillId = @skillId and SkillLevelId = @skillLevelId)          
 end          
end          
         
SELECT *, 0 as TotalDays, 0 as FreeDaysForPerMonthType INTO #tmp                      
FROM dbo.Allocation                       
where              
-- check allocation status and allocation type  
IsDeleted = 0 and --(AllocationType = 'Non-Bill' or AllocationType = 'Bill') and  
--check status and project id                     
 SOWRoleId is not null and SOWRoleId in             
 (             
  select a.Id             
  from dbo.SOWRole a join dbo.SOW b on a.SOWId = b.Id             
  where b.Status in ('Open','Signed','Confirmed','Draft') and (@projectId is null or b.ProjectId=@projectId))                      
--check resource department                      
and ResourceId in (select Id from #tmpResource)              
            
-- check date                      
and (@startDate is null or EndDate is null or EndDate >= @startDate)                       
and (@endDate is null or StartDate <= @endDate)                      
             
UPDATE #tmp                      
Set TotalDays = (                      
 case                       
    when @endDate is null then 1                      
 when StartDate >= @startDate and  EndDate is null then dbo.fn_WorkDays(StartDate, @endDate)                      
 when StartDate < @startDate and EndDate is null then dbo.fn_WorkDays(@startDate, @endDate)                      
 when StartDate >= @startDate and EndDate <= @endDate then dbo.fn_WorkDays(StartDate, EndDate)                      
 when StartDate >=@startDate and EndDate > @endDate then dbo.fn_WorkDays(StartDate, @endDate)                      
when StartDate <@startDate and EndDate >= @endDate then dbo.fn_WorkDays(@startDate, @endDate)                      
 when StartDate <@startDate and EndDate < @endDate then dbo.fn_WorkDays( @startDate, EndDate)                      
 end                      
)                      
UPDATE #tmp                      
Set FreeDaysForPerMonthType = (                      
 case                       
    when @endDate is null then 1                      
 when StartDate >= @startDate and  EndDate is null then dbo.fn_WorkDays(@startDate, StartDate)                      
 when StartDate < @startDate and EndDate is null then 0                  
 when StartDate >= @startDate and EndDate <= @endDate then dbo.fn_WorkDays(@startDate, StartDate) +  dbo.fn_WorkDays(EndDate, @endDate)                       
 when StartDate >=@startDate and EndDate > @endDate then dbo.fn_WorkDays(@startDate, StartDate)                     
when StartDate <@startDate and EndDate >= @endDate then 0                      
 when StartDate <@startDate and EndDate < @endDate then dbo.fn_WorkDays(EndDate, @endDate)                  
 end                      
)          
      
SELECT                       
 a.ResourceId,                       
 sum(case when FTE is null then 0                      
 else convert(float,FTE*TotalDays) end ) as TotalFTEDay,                      
 sum(case when TotalHours is null or dbo.fn_WorkDays(StartDate,EndDate)=0 then 0                      
 else convert(float,(TotalHours*TotalDays))/8/dbo.fn_WorkDays(StartDate,EndDate) end ) as TotalHoursDay,                      
 sum(case when TotalHoursPerMonth is null then 0                      
 else convert(float,TotalHoursPerMonth*TotalDays)/8/22 end ) as TotalHoursPerMonthDay,                  
 sum(TotalHoursPerMonth) as TotalHoursPerMonth                  
 INTO #tmp2                      
FROM  #tmp a                       
GROUP BY ResourceId                      
          
SELECT                       
 a.ResourceId,    
 case when @totalDays=0 then 0 else TotalFTEDay/@totalDays end as FTE,                      
 TotalHoursDay*8 as TotalHours,                      
 TotalHoursPerMonth as TotalHoursPerMonth,                      
 case when @totalDays=0 then 0 else                    
 (CONVERT(float,(case when TotalFTEDay is null then 0 else TotalFTEDay end +                      
 case when TotalHoursDay is null then 0 else TotalHoursDay end +                      
 case when TotalHoursPerMonthDay is null then 0 else TotalHoursPerMonthDay end)/@totalDays)) end as Allocated  INTO #tmp3                      
FROM  #tmp2 a                       
                    
Select               
 a.Id as ResourceId,               
 a.DepartmentId,              
 a.FirstName,               
 a.LastName,               
 a.EmployeeCode,
 users.EmailAddress,
 users.PhoneNumber,
 a.Skype,
 department.Name AS Department,
 case when b.FTE is null then 0 else b.FTE end as FTE,               
 case when b.TotalHours is null then 0 else b.TotalHours end as TotalHours,               
 case when b.TotalHOursPerMonth is null then 0 else b.TotalHoursPerMonth end as TotalHoursPerMonth,               
 case when b.Allocated is null then 0 else b.Allocated end     as Allocated        
 into #tmp4              
from dbo.Resource a 
left join #tmp3 b on a.Id = b.ResourceId 
left join dbo.AbpUsers users on a.UserId = users.Id 
left join dbo.Department department on department.Id = DepartmentId     
          
Select * from #tmp4      
where (Allocated is null or 1-Allocated >= @fte)              
 and (ResourceId in (Select Id from #tmpResource))      
                   
DROP TABLE #tmp                      
DROP TABLE #tmp2                      
DROP TABLE #tmp3                      
DROP TABLE #tmp4                 
DROP TABLE #tmpResource          
                      
END