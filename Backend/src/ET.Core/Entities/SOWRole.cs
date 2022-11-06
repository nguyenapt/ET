using Abp.Domain.Entities.Auditing;

namespace ET.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("SOWRole")]
    public partial class SOWRole : FullAuditedEntity<Guid>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SOWRole()
        {
            Allocations = new HashSet<Allocation>();
        }

        public Guid SOWId { get; set; }

        public bool IsBillable { get; set; }

        [Required]
        [StringLength(50)]
        public string BillingType { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [Required]
        [StringLength(10)]
        public string RateType { get; set; }

        [Required]
        [StringLength(3)]
        public string Currency { get; set; }

        public double StandardRate { get; set; }

        [Required]
        public double ActualRate { get; set; }

        public double? FTE { get; set; }

        public double? TotalHours { get; set; }

        public double? TotalHoursPerMonth { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public int? Term { get; set; }

        public string Description { get; set; }

        public string TimeNote { get; set; }

        public double? EstHoursPerWeek { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ForecastTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Allocation> Allocations { get; set; }

        public Guid? InternalTypeId { get; set; }

        [ForeignKey(nameof(SOWId))]
        public virtual SOW SOW { get; set; }

        [ForeignKey(nameof(InternalTypeId))]
        public virtual InternalType InternalType { get; set; }
    }
}
