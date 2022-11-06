using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ET.Entities
{
    [Table("BillingRate")]
    public partial class BillingRate : Entity<int>
    {
        public string ResourceRole { get; set; }
        public string Currency { get; set; }

        public string MonthlyRate { get; set; }
        public string DailyRate { get; set; }
        public string HourlyRate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EffectiveDate { get; set; }
    }
}
