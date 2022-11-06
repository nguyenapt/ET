namespace ET.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Abp.Domain.Entities;

    [Table("InternalType")]
    public partial class InternalType : Entity<Guid>
    {
        public InternalType()
        {
            SOWRoles = new HashSet<SOWRole>();
        }

        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOWRole> SOWRoles { get; set; }

    }
}
