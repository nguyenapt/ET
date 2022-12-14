using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Abp.Dependency;

namespace ET.Email.Helper
{
    public interface IETEmailHelper : ITransientDependency
    {
        Task<bool> SendMailGenericAsync<T>(
            string emailTemplateType,
            IEnumerable<string> toAddresses,
            IEnumerable<string> toCcAddresses,
            IEnumerable<string> toBccAddresses,
            T modelInput,
            IEnumerable<Attachment> attachments);

        void SendMailAsync<T>(
           string emailTemplateType,
           IEnumerable<string> toAddresses,
           IEnumerable<string> toCcAddresses,
           IEnumerable<string> toBccAddresses,
           T modelInput,
           IEnumerable<Attachment> attachments);
    }
}
