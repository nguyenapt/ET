using System;

namespace ET.Clients.Dto
{
    public class ClientResultRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string ClientCode { get; set; }
        public Guid? KAMResourceId { get; set; }
        public Guid? PMOId { get; set; }
    }
}
