using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ET.Entities
{
    [Table("SkillLevel")]
    public partial class SkillLevel : Entity<Guid>
    {
        public string Level { get; set; }
        public string Description { get; set; }
    }
}
