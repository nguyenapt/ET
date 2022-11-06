using System;

namespace ET.SOWRoles.Dto
{
    public interface ISowRoleDto
    {
        public bool IsBillable { get; set; }
        public string BillingType { get; set; }
        public string RoleName { get; set; }
        public string RateType { get; set; }
        public string Currency { get; set; }
        public double StandardRate { get; set; }
        public double? ActualRate { get; set; }
        public double? FTE { get; set; }
        public double? TotalHours { get; set; }
        public double? TotalHoursPerMonth { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Term { get; set; }
        public string Description { get; set; }
        public string TimeNote { get; set; }
        public Guid? InternalTypeId { get; set; }
    }
}
