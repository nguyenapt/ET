using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using ET.Authorization;
using ET.Clients.Dto;
using ET.Entities;
using ET.Projects;
using ET.Projects.Dto;
using ET.SoW;
using Task = System.Threading.Tasks.Task;

namespace ET.Clients
{
    [AbpAuthorize(PermissionNames.Pages_Clients)]
    public class ClientAppService : AsyncCrudAppService<Client, ClientDto, Guid, ClientResultRequestDto, CreateClientDto, ClientDto>, IClientAppService
    {
        private readonly IProjectAppService _projectAppService;
        private readonly ISowAppService _sowAppService;
        public ClientAppService(IRepository<Client, Guid> repository, IProjectAppService projectAppService, ISowAppService sowAppService) : base(repository)
        {
            _projectAppService = projectAppService;
            _sowAppService = sowAppService;
            LocalizationSourceName = ETConsts.LocalizationSourceName;
        }

        public override Task<ClientDto> CreateAsync(CreateClientDto input)
        {
            input.ClientCode = input.ClientCode?.Trim();
            return !string.IsNullOrEmpty(input.ClientCode) && ClientCodeIsNotUnique(input.ClientCode, null) 
                ? Task.FromException<ClientDto>(new Exception(L("ClientCodeIsUnique"))) 
                : base.CreateAsync(input);
        }

        public override Task<ClientDto> UpdateAsync(ClientDto input)
        {
            input.ClientCode = input.ClientCode?.Trim();
            return !string.IsNullOrEmpty(input.ClientCode) && ClientCodeIsNotUnique(input.ClientCode, input.Id) 
                ? Task.FromException<ClientDto>(new Exception(L("ClientCodeIsUnique"))) 
                : base.UpdateAsync(input);
        }

        public override Task DeleteAsync(EntityDto<Guid> input)
        {
            var clientUsedInProject = _projectAppService.GetAllAsync(new ProjectResultRequestDto
            {
                ClientId = input.Id,
                PageSize = 1,
                CurrentPage = 1
            }).Result.TotalCount;

            if (clientUsedInProject > 0) return Task.FromException(new Exception(L("ItemIsUsedInProject")));

            return base.DeleteAsync(input);
        }

        protected override Task<Client> GetEntityByIdAsync(Guid id)
        {
            var client = Repository.GetAllIncluding(x => x.PMOResource, x => x.KAMResource)
                .FirstOrDefault(x => x.Id == id);

            return Task.FromResult(client);
        }

        protected override IQueryable<Client> ApplySorting(IQueryable<Client> query, ClientResultRequestDto input)
        {
            return query.OrderByDescending(x => x.CreationTime);
        }

        protected override IQueryable<Client> CreateFilteredQuery(ClientResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Projects, x=>x.KAMResource, x=> x.PMOResource)
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
                .WhereIf(!input.Email.IsNullOrWhiteSpace(), x => x.Email.Contains(input.Email))
                .WhereIf(!input.Address.IsNullOrWhiteSpace(), x => x.Address.Contains(input.Address))
                .WhereIf(!input.Website.IsNullOrWhiteSpace(), x => x.Website.Contains(input.Website))
                .WhereIf(!input.ClientCode.IsNullOrWhiteSpace(), x => x.ClientCode.Contains(input.ClientCode))
                .WhereIf(input.KAMResourceId.HasValue, x=> x.KAMResourceId == input.KAMResourceId)
                .WhereIf(input.PMOId.HasValue, x=> x.PMOId == input.PMOId);
        }

        private bool ClientCodeIsNotUnique(string clientCode, Guid? clientId)
        {
            return Repository.GetAll()
                .AsEnumerable()
                .WhereIf(clientId.HasValue, x=> x.Id != clientId.Value).Any(x => clientCode.Equals(x.ClientCode, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}

