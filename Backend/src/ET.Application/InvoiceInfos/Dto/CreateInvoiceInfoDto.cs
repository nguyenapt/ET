using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.InvoiceInfos.Dto
{
    [AutoMapTo(typeof(InvoiceInfo))]
    public class CreateInvoiceInfoDto
    {
        public string Name { get; set; }
    }
}
