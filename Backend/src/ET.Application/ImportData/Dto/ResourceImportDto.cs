using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ET.ImportData.Dto
{
    [AutoMapTo(typeof(Entities.Resource))]
    public class ResourceImportDto
    {
        [Column("UserId")]
        public long? UserId { get; set; }
        [Column("FirstName")]
        public string FirstName { get; set; }
        [Column("LastName")]
        public string LastName { get; set; }
        [Column("EmployeeCode")]
        public string EmployeeCode { get; set; }
        [Column("TimeStamp")]
        public DateTime TimeStamp { get; set; }
 
        [Column("Country")]
        public string Country { get; set; }
        [Column("DepartmentId")]
        public Guid? DepartmentId { get; set; }
        [Column("WorkingHourRuleId")]
        public Guid? WorkingHourRuleId { get; set; }
        [Column("IsKAM")]
        public bool IsKAM { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }
        public string DepartmentName { get; set; }
        public string WorkingHourRuleName { get; set; }  
        public string JobTitle { get; set; }   
        public string JobTitleLevel { get; set; }
    }
}
