using Abp.Domain.Entities.Auditing;

namespace ET.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("SOW")]
    public partial class SOW : FullAuditedEntity<Guid>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SOW()
        {
            SOWRoles = new HashSet<SOWRole>();
            SowStatusNotes = new HashSet<SowStatusNote>();
        }

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public string FileUrl { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public string Status { get; set; }

        [StringLength(50)]
        public string ClientPONumber { get; set; }

        public string Description { get; set; }

        public int? SowNumber { get; set; }
        public decimal? Version { get; set; }

        [StringLength(50)]
        public string InvoicingCycle { get; set; }

        [Column("1stInvoiceDate", TypeName = "date")]
        public DateTime? FirstInvoiceDate { get; set; }

        public Guid? BeneficiaryId { get; set; }

        [ForeignKey(nameof(BeneficiaryId))]
        public virtual Beneficiary Beneficiary { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOWRole> SOWRoles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SowStatusNote> SowStatusNotes { get; set; }
    }
}
