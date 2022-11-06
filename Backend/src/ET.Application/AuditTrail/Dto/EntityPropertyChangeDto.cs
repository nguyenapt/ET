using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.EntityHistory;

namespace ET.AuditTrail.Dto
{
    [AutoMapFrom(typeof(EntityPropertyChange))]
    [AutoMapTo(typeof(EntityPropertyChange))]
    public class EntityPropertyChangeDto : EntityDto<long>
    {        
        public string NewValue { get; set; }
        public string OriginalValue { get; set; }

        public string PropertyName { get; set; }

        public string PropertyTypeFullName { get; set; }
    }
}
