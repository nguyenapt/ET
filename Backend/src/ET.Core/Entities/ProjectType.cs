using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ET.Entities
{
    [Table("ProjectType")]
    public partial class ProjectType : Entity<int>
    {
        public ProjectType()
        {
            Projects = new HashSet<Project>();
        }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Scope { get; set; }

        [Column("P/L")]
        public bool PL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Project> Projects { get; set; }
    }
}
