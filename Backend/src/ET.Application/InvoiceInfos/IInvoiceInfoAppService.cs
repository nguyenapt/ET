using System;
using Abp.Application.Services;
using ET.InvoiceInfos.Dto;
using ET.SoW.Dto;

namespace ET.InvoiceInfos
{
    public interface IInvoiceInfoAppService : IAsyncCrudAppService<InvoiceInfoDto, Guid, InvoiceInfoResultRequestDto, CreateInvoiceInfoDto, InvoiceInfoDto>
    {
    }
}


