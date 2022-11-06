using System;

namespace ET.Allocations.Dto
{
    public class AllocationForResourceDto
    {
        public Guid AllocationId { get; set; }
        public string Project { get; set; }
        public string Program { get; set; }
        public string Role { get; set; }
        public bool IsBillable { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
