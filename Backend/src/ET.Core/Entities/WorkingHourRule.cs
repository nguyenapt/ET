namespace ET.Entities
{
    using Abp.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("WorkingHourRule")]
    public partial class WorkingHourRule : Entity<Guid>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkingHourRule()
        {
            Resources = new HashSet<Resource>();
        }

        [StringLength(50)]
        public string Name { get; set; }

        public float? RequiredMondayHours { get; set; }

        public float? RequiredTuesdayHours { get; set; }

        public float? RequiredWednesdayHours { get; set; }

        public float? RequiredThursdayHours { get; set; }

        public float? RequiredFridayHours { get; set; }

        public float? RequiredSaturdayHours { get; set; }

        public float? RequiredSundayHours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
