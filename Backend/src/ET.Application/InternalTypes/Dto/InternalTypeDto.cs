using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using ET.Entities;

namespace ET.InternalTypes.Dto
{
    [AutoMapFrom(typeof(InternalType))]
    [AutoMapTo(typeof(InternalType))]
    public class InternalTypeDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
