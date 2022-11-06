using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.AllocationTypes.Dto
{
    [AutoMapFrom(typeof(AllocationType))]
    [AutoMapTo(typeof(AllocationType))]
    public class AllocationTypeDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public bool? IsSupporter { get; set; }
    }
}
