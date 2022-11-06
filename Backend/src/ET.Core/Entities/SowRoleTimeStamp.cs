using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp;
using Abp.Domain.Entities;

namespace ET.Entities
{
    public class SowRoleTimeStamp : Entity<Guid> , IShouldInitialize
    {
        public Guid SowRoleId { get; set; }
        [ForeignKey(nameof(SowRoleId))]
        public SOWRole SowRole { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        public double ActualRate { get; set; }
        public double EstHoursPerWeek { get; set; }
        public double Estimate { get; set; }
        public void Initialize()
        {
            Id = Guid.NewGuid();
        }
    }
}
