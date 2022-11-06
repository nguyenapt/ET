using System;

namespace ET.Allocations.Dto
{
    public interface IAllocationDto
    {
        public bool IsBillable { get; set; }

        public Guid SOWRoleId { get; set; }

        public Guid ResourceId { get; set; }
        public string RateType { get; set; }

        public double? FTE { get; set; }

        public double? TotalHours { get; set; }

        public double? TotalHoursPerMonth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; }
        public string TimeNote { get; set; }
        public int? AllocationTypeId { get; set; }
    }
}
