using System;

namespace ET.Allocations.Dto
{
    public class SOWAllocationStatusRequestDto
    {
        public Guid? DepartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
