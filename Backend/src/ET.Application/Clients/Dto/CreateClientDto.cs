using System;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.Clients.Dto
{
    [AutoMapTo(typeof(Client))]
    public class CreateClientDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string ClientCode { get; set; }

        public Guid? KAMResourceId { get; set; }
        public Guid? PMOId { get; set; }
    }
}
