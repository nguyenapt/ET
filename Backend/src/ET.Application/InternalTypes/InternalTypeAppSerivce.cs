using Abp.Application.Services;
using Abp.Domain.Repositories;
using ET.Entities;
using ET.InternalTypes.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ET.InternalTypes
{
    public class InternalTypeAppSerivce : AsyncCrudAppService<InternalType, InternalTypeDto, Guid, InternalTypeResultRequestDto, CreateInternalTypeDto, InternalTypeDto>, IInternalTypeAppService
    {
        private readonly InternalTypeRepository _internalTypeRepository;

        public InternalTypeAppSerivce(IRepository<InternalType, Guid> repository,
            InternalTypeRepository internalTypeRepository) : base(repository)
        {
            _internalTypeRepository = internalTypeRepository;
        }

        public async Task<List<InternalTypeDto>> GetTypeSupporter()
        {
            var result = await _internalTypeRepository.GetInternalTypeSupporter();
            return result;
        }
    }
}
