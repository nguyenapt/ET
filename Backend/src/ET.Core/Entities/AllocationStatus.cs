using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ET.Entities
{
    [Table("AllocationStatus")]
    public partial class AllocationStatus : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
