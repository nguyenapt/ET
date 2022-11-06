using Abp.AutoMapper;
using ET.Entities;

namespace ET.InternalTypes.Dto
{
    [AutoMapTo(typeof(InternalType))]
    public class CreateInternalTypeDto
    {
        public string Name { get; set; }
    }
}
