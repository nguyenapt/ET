using System;

namespace ET.Entities
{
    using Abp.Domain.Entities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("Beneficiary")]
    public partial class Beneficiary : Entity<Guid>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Beneficiary()
        {
            SOWs = new HashSet<SOW>();
        }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public string Detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOW> SOWs { get; set; }
    }
}
