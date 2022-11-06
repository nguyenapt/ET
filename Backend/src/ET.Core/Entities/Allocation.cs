namespace ET.Entities
{
    using Abp.Domain.Entities.Auditing;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("Allocation")]
    public partial class Allocation : FullAuditedEntity<Guid>
    {
        public Allocation()
        {
            TimesheetEntries = new HashSet<TimesheetEntry>();
        }

        public bool IsBillable { get; set; }

        public Guid SOWRoleId { get; set; }

        public Guid ResourceId { get; set; }

        [Required]
        [StringLength(10)]
        public string RateType { get; set; }

        public double? FTE { get; set; }

        public double? TotalHours { get; set; }

        public double? TotalHoursPerMonth { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string TimeNote { get; set; }

        public int? AllocationTypeId { get; set; }

        [Range(0, double.MaxValue)]
        public double? EstHoursPerWeek { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ForecastTime { get; set; }

        public int? AllocationStatusId { get; set; }

        [ForeignKey(nameof(ResourceId))]
        public virtual Resource Resource { get; set; }
        public virtual SOWRole SOWRole { get; set; }     

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimesheetEntry> TimesheetEntries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AllocationTimeStamp> AllocationTimeStamps { get; set; }

        [ForeignKey(nameof(AllocationStatusId))]
        public virtual AllocationStatus AllocationStatus { get; set; }

        [ForeignKey(nameof(AllocationTypeId))]
        public virtual AllocationType AllocationType { get; set; }
    }
}
