using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.EntityHistory;
using Abp.Events.Bus.Entities;

namespace ET.AuditTrail.Dto
{
    [AutoMapFrom(typeof(EntityChange))]
    [AutoMapTo(typeof(EntityChange))]
    public class EntityChangeDto : EntityDto<long>
    {
        public EntityChangeType ChangeType { get; set; }
        public string EntityId { get; set; }
        public string EntityTypeFullName { get; set; }

        public ICollection<EntityPropertyChangeDto> PropertyChanges { get; set; }
    }
}
