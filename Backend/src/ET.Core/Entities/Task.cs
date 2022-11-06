namespace ET.Entities
{
    using Abp.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("Task")]
    public partial class Task : Entity<Guid>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            TimesheetEntries = new HashSet<TimesheetEntry>();
        }

        [StringLength(50)]
        public string Name { get; set; }

        public Guid? TaskCategoryId { get; set; }

        [ForeignKey(nameof(TaskCategoryId))]
        public virtual TaskCategory TaskCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimesheetEntry> TimesheetEntries { get; set; }
    }
}
