namespace ET.Entities
{
    using Abp.Domain.Entities;
    using ET.Authorization.Roles;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("TaskCategory")]
    public partial class TaskCategory : Entity<Guid>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaskCategory()
        {
            Tasks = new HashSet<Task>();
        }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public string RoleName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
