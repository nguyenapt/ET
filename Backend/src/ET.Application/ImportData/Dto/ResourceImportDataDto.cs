using Npoi.Mapper.Attributes;

namespace ET.ImportData.Dto
{
    public class ResourceImportData
    {
        [Column("No.")]
        public string No { get; set; }

        [Column("User name")]
        public string UserName { get; set; }

        [Column("First name")]
        public string FirstName { get; set; }

        [Column("Last name")]
        public string LastName { get; set; }

        [Column("Employee Code")]
        public string EmployeeCode { get; set; }

        [Column("Country")]
        public string Country { get; set; }

        [Column("Department Name")]
        public string DepartmentName { get; set; }

        [Column("Working hour rule name")]
        public string WorkingHourRuleName { get; set; }

        [Column("Job Title")]
        public string JobTitle { get; set; }

        [Column("Job Title Level")]
        public string JobTitleLevel { get; set; }
    }
}
