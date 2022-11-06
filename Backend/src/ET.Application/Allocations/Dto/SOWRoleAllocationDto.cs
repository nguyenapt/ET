namespace ET.Allocations.Dto
{
    public class SowRoleAllocationDto : AllocationDto
    {
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string AllocationType { get; set; }

        public string AllocationStatus { get; set; }
    }
}
