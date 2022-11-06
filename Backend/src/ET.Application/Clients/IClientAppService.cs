using System;
using Abp.Application.Services;
using ET.Clients.Dto;

namespace ET.Clients
{
    public interface IClientAppService : IAsyncCrudAppService<ClientDto, Guid, ClientResultRequestDto, CreateClientDto, ClientDto>
    {
    }
}


