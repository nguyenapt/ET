using System;

namespace ET.AuditTrail.Dto
{
    public class EntityChangeSetResultRequestDto
    {
        public string ClientIpAddress { get; set; }
        public string ClientName { get; set; }
        public DateTime CreationTime { get; set; }
        public long? UserId { get; set; }
        public string Reason { get; set; }
    }
}
