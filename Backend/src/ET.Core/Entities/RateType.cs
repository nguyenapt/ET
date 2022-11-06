using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ET.Entities
{
    [Table("RateType")]
    public partial class RateType : Entity<int>
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
