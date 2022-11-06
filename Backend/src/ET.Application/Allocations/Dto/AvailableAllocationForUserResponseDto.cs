using System;

namespace ET.Allocations.Dto
{
    public class AvailableAllocationForUserResponseDto
    {
        public Guid? AllocationId { get; set; }
        public string SowCode { get; set; }
        public string SowRoleId { get; set; }
        public Guid? ProjectManagerId { get; set; }
        public string ProjectName { get; set; }
        public string TimeNote { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalHours { get; set; }
        public int TotalHoursPerMonth { get; set; }
        public int FTE { get; set; }
        public string RoleName { get; set; }
    }
}
