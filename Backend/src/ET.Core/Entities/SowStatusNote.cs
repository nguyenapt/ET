using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace ET.Entities
{
    public class SowStatusNote : AuditedEntity<Guid>
    {
        public Guid SowId { get; set; }
        public string Status { get; set; }
        public string StatusNote { get; set; }

        [ForeignKey(nameof(SowId))]
        public SOW Sow { get; set; }
    }
}
