using System;

namespace ET.Allocations.Dto
{
    public class AvailableResourceDto
    {
        public Guid ResourceId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeCode { get; set; }
        public double FTE { get; set; }
        public double? TotalHours { get; set; }
        public double? TotalHoursPerMonth { get; set; }
        public double Allocated { get; set; }
        public double AvailableFTE => 1 - FTE;
        public string EmailAddress { get; set; }
        public string Skype { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
    }
}
