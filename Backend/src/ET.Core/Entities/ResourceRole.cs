using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ET.Entities
{
    [Table("ResourceRole")]
    public partial class ResourceRole : Entity<int>
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
