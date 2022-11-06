using Abp.Application.Services;
using ET.InternalTypes.Dto;
using System;

namespace ET.InternalTypes
{
    public interface IInternalTypeAppService : IAsyncCrudAppService<InternalTypeDto, Guid, InternalTypeResultRequestDto, CreateInternalTypeDto, InternalTypeDto>
    {
    }
}
