using Abp.Domain.Entities.Auditing;

namespace ET.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("Client")]
    public partial class Client : FullAuditedEntity<Guid>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Projects = new HashSet<Project>();
        }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        public string Website { get; set; }
        [StringLength(50)]
        public string ClientCode { get; set; }

        public Guid? KAMResourceId { get; set; }

        [ForeignKey(nameof(KAMResourceId))]
        public virtual Resource KAMResource { get; set; }

        public Guid? PMOId { get; set; }

        [ForeignKey(nameof(PMOId))]
        public virtual Resource PMOResource { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Projects { get; set; }
    }
}
