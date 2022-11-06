using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Abp.Domain.Entities;
using ET.Authorization.Roles;
using ET.Authorization.Users;

namespace ET.Entities
{
    [Table("Resource")]
    public class Resource : Entity<Guid>
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Resource()
        {
            Allocations = new HashSet<Allocation>();
            LeavePermissions = new HashSet<LeavePermission>();
            TimesheetEntries = new HashSet<TimesheetEntry>();
            ResourceSkills = new HashSet<ResourceSkill>();
            LeaveBanks = new HashSet<LeaveBank>();
        }

        public long? UserId { get; set; }

        public string UserName { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string EmployeeCode { get; set; }
        public string JobTitle { get; set; }
        public string JobTitleLevel { get; set; }

        public DateTime TimeStamp { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }        

        [StringLength(50)]
        public string Country { get; set; }

        public Guid? DepartmentId { get; set; }

        public Guid? WorkingHourRuleId { get; set; }

        public bool IsKAM { get; set; }

        public string Skype { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Allocation> Allocations { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual Department Department { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeavePermission> LeavePermissions { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveBank> LeaveBanks { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimesheetEntry> TimesheetEntries { get; set; }

        public virtual WorkingHourRule WorkingHourRule { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResourceSkill> ResourceSkills { get; set; }

        public virtual bool? IsActive { get; set; }
    }
}
