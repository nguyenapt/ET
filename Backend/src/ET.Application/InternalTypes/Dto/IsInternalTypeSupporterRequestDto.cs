using System;

namespace ET.InternalTypes
{
    public class IsInternalTypeSupporterRequestDto
    {
        public IsInternalTypeSupporterRequestDto(Guid? internalTypeId)
        {
            InternalTypeId = internalTypeId;
        }

        public Guid? InternalTypeId { get; set; }
    }
}
