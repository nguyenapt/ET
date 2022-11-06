using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ET.Entities
{    
    [Table("LeavePermission")]
    public partial class LeavePermission : Entity<Guid>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LeavePermission()
        {
            TimesheetEntries = new HashSet<TimesheetEntry>();
        }
        
        public Guid ResourceId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public decimal TotalHours { get; set; }

        [Required]
        public Guid LeaveTypeId { get; set; }

        [Required]
        public bool IsFullDay { get; set; }        

        public byte? ApprovalStatus { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public DateTime CreationTime { get; set; }

        public string Reason { get; set; }

        public string RejectReason { get; set; }
         
        [ForeignKey(nameof(ResourceId))]
        public virtual Resource Resource { get; set; }

        [ForeignKey(nameof(LeaveTypeId))]
        public virtual LeaveType LeaveType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimesheetEntry> TimesheetEntries { get; set; }     
    }
}
