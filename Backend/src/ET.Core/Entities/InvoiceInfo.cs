namespace ET.Entities
{
    using Abp.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("InvoiceInfo")]
    public partial class InvoiceInfo : Entity<Guid>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InvoiceInfo()
        {
            SOWs = new HashSet<SOW>();
        }

        [Required]
        [StringLength(250)]
        public string InvoiceName { get; set; }

        [StringLength(250)]
        public string InvoiceAddress { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOW> SOWs { get; set; }
    }
}
