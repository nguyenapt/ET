namespace ET.Entities
{
    using Abp.Domain.Entities;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("ResourceSkill")]
    public partial class ResourceSkill : Entity<Guid>
    {
        [Column(Order = 0)]
        public Guid ResourceId { get; set; }

        [Column(Order = 1)]
        public Guid SkillId { get; set; }

        public Guid SkillLevelId { get; set; }

        [ForeignKey(nameof(SkillLevelId))]
        public virtual SkillLevel SkillLevel { get; set; }

        [ForeignKey(nameof(ResourceId))]
        public virtual Resource Resource { get; set; }

        [ForeignKey(nameof(SkillId))]
        public virtual Skill Skill { get; set; }
    }
}
