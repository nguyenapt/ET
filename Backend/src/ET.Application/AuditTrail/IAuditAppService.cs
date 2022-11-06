using ET.AuditTrail.Dto;
using System.Threading.Tasks;

namespace ET.AuditTrail
{
    public interface IAuditAppService
    {
        public Task CreateAsync(EntityChangeSetDto input);
    }
}
