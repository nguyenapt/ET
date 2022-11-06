using System;

namespace ET.Allocations.Dto
{
    public class AllocationResultRequestDto
    {
        public Guid? DepartmentId { get; set; }
        public Guid? ProjectId { get; set; }
        public string ResourceName { get; set; }
        public Guid? SkillId { get; set; }
        public Guid? SkillLevelId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double FTE { get; set; }
    }
}
