using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.InvoiceInfos.Dto
{
    [AutoMapFrom(typeof(InvoiceInfo))]
    [AutoMapTo(typeof(InvoiceInfo))]
    public class InvoiceInfoDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
