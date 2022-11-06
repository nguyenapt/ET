using Abp.AutoMapper;
using ET.Entities;

namespace ET.AllocationTypes.Dto
{
    [AutoMapTo(typeof(AllocationType))]
    public class CreateAllocationTypeDto
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public bool? IsSupporter { get; set; }
    }
}
