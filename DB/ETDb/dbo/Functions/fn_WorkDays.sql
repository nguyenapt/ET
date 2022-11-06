  
CREATE FUNCTION dbo.fn_WorkDays(@StartDate DATETIME, @EndDate DATETIME= NULL )    
RETURNS INT     
AS    
BEGIN    
       DECLARE @Days int    
       SET @Days = 0    
    
       IF @EndDate = NULL    
              SET @EndDate = EOMONTH(@StartDate) --last date of the month    
    
       WHILE DATEDIFF(dd,@StartDate,@EndDate) >= 0    
       BEGIN    
              IF DATENAME(dw, @StartDate) <> 'Saturday'     
                     and DATENAME(dw, @StartDate) <> 'Sunday'     
                     and Not ((Day(@StartDate) = 1 And Month(@StartDate) = 1)) --New Year's Day.    
                     and Not ((Day(@StartDate) = 2 And Month(@StartDate) = 9)) --Independence Day.    
              BEGIN    
                     SET @Days = @Days + 1    
              END    
    
              SET @StartDate = DATEADD(dd,1,@StartDate)    
       END    
    
       RETURN  @Days    
END