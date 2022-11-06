using System;

namespace ET.Allocations.Dto
{
    public class SOWAllocationStatusDto
    {
        public Guid SOWId { get; set; }
        public Guid SOWRoleId { get; set; }
       
        public double FTEDemand { get; set; }
        public double TotalHoursDemand { get; set; }
        public double TotalHoursPerMonthDemand { get; set; }
        public double FTEAllocate { get; set; }
        public double TotalHoursAllocate { get; set; }
        public double TotalHoursPerMonthAllocate { get; set; }
    }
}
