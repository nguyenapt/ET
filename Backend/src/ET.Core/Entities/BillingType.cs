using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ET.Entities
{
    [Table("BillingType")]
    public partial class BillingType : Entity<int>
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
