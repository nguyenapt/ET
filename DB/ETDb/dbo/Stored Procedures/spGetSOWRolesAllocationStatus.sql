
CREATE PROCEDURE [dbo].[spGetSOWRolesAllocationStatus]                           
 @sowId uniqueidentifier             
AS                              
BEGIN                              
      
SELECT a.*, 0 as ActualHours, convert(nvarchar(20),'') as AllocationStatus INTO #tmpSOWRole 
FROM dbo.SOWRole a JOIN 
dbo.SOW b on a.SOWId = b.id 
WHERE SOWId = @sowId and b.Status in ('Open','Signed','Confirmed')      
and a.IsDeleted = 'false' 
and NOT EXISTS (select top 1 * from dbo.InternalType internalType where internalType.Id = a.InternalTypeId)
UPDATE #tmpSOWRole       
SET ActualHours = (CASE WHEN FTE IS NOT NULL THEN FTE*8*dbo.fn_WorkDays(StartDate, EndDate) ELSE 0 END)      
WHERE StartDate is not null and EndDate is not null       
and BillingType in ('MRT-C','TMFT-T','TMFT-C','TMFT-TB','TMFT-CB') 
      
UPDATE #tmpSOWRole       
SET ActualHours = (CASE       
      WHEN FTE IS NOT NULL THEN FTE*8*dbo.fn_WorkDays(StartDate, EndDate)       
      ELSE       
           CASE WHEN EstHoursPerWeek IS NOT NULL THEN EstHoursPerWeek*dbo.fn_WorkDays(StartDate, EndDate)/5       
          ELSE CASE WHEN TotalHours IS NOT NULL THEN TotalHours ELSE 0 END      
        END      
      END)      
WHERE StartDate is not null and EndDate is not null       
and BillingType in ('TMPT-T')      
      
      
UPDATE #tmpSOWRole       
SET ActualHours = (CASE       
      WHEN FTE IS NOT NULL THEN FTE*8*dbo.fn_WorkDays(StartDate, EndDate)       
      ELSE       
           CASE WHEN EstHoursPerWeek IS NOT NULL THEN EstHoursPerWeek*dbo.fn_WorkDays(StartDate, EndDate)/5       
          ELSE CASE WHEN TotalHoursPerMonth IS NOT NULL THEN TotalHoursPerMonth*dbo.fn_WorkDays(StartDate, EndDate)/dbo.fn_WorkDays(DATEFROMPARTS(YEAR(StartDate), MONTH(StartDate),1), DATEFROMPARTS(YEAR(StartDate), MONTH(StartDate),DAY(EOMONTH(StartDate)))) ELSE 0 END      
        END      
      END)      
WHERE StartDate is not null and EndDate is not null       
and BillingType in ('TMPT-C')      
      
--AND        
UPDATE #tmpSOWRole       
SET ActualHours = (CASE WHEN TotalHours IS NOT NULL THEN TotalHours ELSE 0 END)      
WHERE StartDate is not null and EndDate is not null       
AND BillingType in ('TMPT-TB')      
--AND        
UPDATE #tmpSOWRole       
SET ActualHours = (CASE WHEN TotalHoursPerMonth IS NOT NULL THEN TotalHoursPerMonth*dbo.fn_WorkDays(StartDate, EndDate)/dbo.fn_WorkDays(DATEFROMPARTS(YEAR(StartDate), MONTH(StartDate),1), DATEFROMPARTS(YEAR(StartDate), MONTH(StartDate),DAY(EOMONTH(StartDate)))) ELSE 0 END)      
WHERE StartDate is not null and EndDate is not null       
AND BillingType in ('TMPT-CB')      

--AND     


SELECT       
 a.*,      
 CASE WHEN a.StartDate is null or a.StartDate < b.StartDate then b.StartDate else a.StartDate end as ActualStartDate,      
 CASE WHEN a.EndDate is null or a.EndDate > b.EndDate then b.EndDate else a.EndDate end as ActualEndDate,      
 0 as ActualTotalDays       
 INTO #tmpAllocation       
 FROM dbo.Allocation a join #tmpSOWRole b on a.SOWRoleId = b.Id       
 WHERE b.StartDate is not null and b.EndDate is not null       
       
UPDATE #tmpAllocation       
 SET ActualTotalDays = dbo.fn_WorkDays(ActualStartDate, ActualEndDate)      
      
SELECT       
 SOWRoleId,      
 SUM(      
  --total hours for fte type      
  CASE WHEN FTE IS NULL THEN 0 ELSE FTE*ActualTotalDays*8 END +      
  --total hours for total hours type      
  CASE WHEN TotalHours IS NULL THEN 0 ELSE TotalHours END +      
  --total hours for total hours per month type      
  CASE WHEN TotalHoursPerMonth IS NULL THEN 0 ELSE TotalHoursPerMonth*ActualTotalDays/dbo.fn_WorkDays(DATEFROMPARTS(YEAR(StartDate), MONTH(StartDate),1), DATEFROMPARTS(YEAR(StartDate), MONTH(StartDate),DAY(EOMONTH(StartDate)))) END      
 ) As ActualAllocatedHours      
INTO #tmpSOWAllocation      
FROM #tmpAllocation       
GROUP BY SOWRoleId      
            
                  
SELECT       
 A.*, CASE WHEN B.ActualAllocatedHours IS NOT NULL THEN B.ActualAllocatedHours ELSE -1 END AS ActualAllocatedHours, CONVERT(float,0) AS GapPercentage      
 INTO #tmpResult      
FROM #tmpSOWRole A LEFT JOIN #tmpSOWAllocation B ON A.Id=B.SOWRoleId      
      
UPDATE #tmpResult      
SET GapPercentage =CONVERT(decimal, (ActualAllocatedHours - ActualHours))*100/ActualHours      
WHERE StartDate IS NOT NULL AND EndDate IS NOT NULL AND ActualHours>0 AND  ActualAllocatedHours >=0      
    
    
UPDATE #tmpResult      
SET AllocationStatus = (CASE WHEN GapPercentage>=-5 and GapPercentage<=5 THEN 'GOOD' ELSE CASE WHEN GapPercentage > 5 THEN 'OVER' ELSE 'UNDER' END END)      
WHERE StartDate IS NOT NULL AND EndDate IS NOT NULL AND ActualHours>0 AND  ActualAllocatedHours >=0      
      
SELECT * FROM #tmpResult      
      
DROP TABLE #tmpAllocation                              
DROP TABLE #tmpSOWRole                              
DROP TABLE #tmpSOWAllocation                              
                  
                              
END