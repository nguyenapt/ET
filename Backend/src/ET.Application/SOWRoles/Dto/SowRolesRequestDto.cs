using System;

namespace ET.SOWRoles.Dto
{
    public class SowRolesRequestDto
    {
        public SowRolesRequestDto(Guid sowId)
        {
            SowId = sowId;
        }

        public SowRolesRequestDto()
        {
        }

        public Guid SowId { get; set; }
    }
}
