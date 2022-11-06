using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ET.Entities
{    
    [Table("LeaveBank")]
    public partial class LeaveBank : Entity<Guid>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LeaveBank()
        {            
        }
        [Required]
        public Guid ResourceId { get; set; }
        [Required]
        public decimal TotalAllowedHours { get; set; }
        [Required]
        public int Year { get; set; }

        [Required]
        public Guid LeaveTypeId { get; set; }
               
        [ForeignKey(nameof(ResourceId))]
        public virtual Resource Resource { get; set; }

        [ForeignKey(nameof(LeaveTypeId))]
        public virtual LeaveType LeaveType { get; set; }   
    }
}
