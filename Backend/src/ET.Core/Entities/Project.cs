using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ET.Entities
{
    [Table("Project")]
    public partial class Project : FullAuditedEntity<Guid>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            SOWs = new HashSet<SOW>();
        }

        public Guid? ClientId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public Guid? DepartmentId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UniqueCode { get; set; }

        [StringLength(65)]
        public string ProjectTag { get; set; }
        public string ProjectCode { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual Client Client { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual Department Department { get; set; }

        public Guid? ProjectManagerId { get; set; }

        [ForeignKey(nameof(ProjectManagerId))]
        public virtual Resource ProjectManager { get; set; }

        public Guid? PMOId { get; set; }

        [ForeignKey(nameof(PMOId))]
        public virtual Resource PMOResource { get; set; }

        public int? ProjectTypeId { get; set; }

        [ForeignKey(nameof(ProjectTypeId))]
        public virtual ProjectType ProjectType { get; set; }
        
        public int? ProjectStateId { get; set; }

        [ForeignKey(nameof(ProjectStateId))]
        public virtual ProjectStateType ProjectState { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOW> SOWs { get; set; }
    }
}
