using System.Threading.Tasks;
using Abp.EntityHistory;
using Abp.ObjectMapping;
using ET.AuditTrail.Dto;

namespace ET.AuditTrail
{    
    public class AuditAppService : ETAppServiceBase, IAuditAppService
    {
        private readonly IEntityHistoryStore _entityHistoryStore;
        private readonly IObjectMapper _objectMapper;

        public AuditAppService(IEntityHistoryStore entityHistoryStore, IObjectMapper objectMapper) 
        {      
            _entityHistoryStore = entityHistoryStore;
            _objectMapper = objectMapper;
        }

        public Task CreateAsync(EntityChangeSetDto input)
        {
            var changeset = _objectMapper.Map<EntityChangeSet>(input);  

            return _entityHistoryStore.SaveAsync(changeset);                        
        }
    }
}

