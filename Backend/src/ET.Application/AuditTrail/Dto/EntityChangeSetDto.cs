using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.EntityHistory;

namespace ET.AuditTrail.Dto
{
    [AutoMapFrom(typeof(EntityChangeSet))]
    [AutoMapTo(typeof(EntityChangeSet))]
    public class EntityChangeSetDto : EntityDto<long>
    {
        public string ClientIpAddress { get; set; }
        public string ClientName { get; set; }
        public DateTime CreationTime { get; set; }
        public long? UserId { get; set; }
        public string Reason { get; set; }

        public IList<EntityChangeDto> EntityChanges { get; set; }
    }
}
