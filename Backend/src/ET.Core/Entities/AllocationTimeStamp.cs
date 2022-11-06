using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ET.Entities
{
    [Table("AllocationTimeStamp")]
    public partial class AllocationTimeStamp : Entity<Guid>
    {
        public Guid AllocationId { get; set; }
        [ForeignKey(nameof(AllocationId))]
        public Allocation Allocation { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        public double ActualRate { get; set; }
        public double EstHoursPerWeek { get; set; }
        public double Estimate { get; set; }
    }
}
