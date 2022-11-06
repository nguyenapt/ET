using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;
using ET.Resources.Dto;

namespace ET.Clients.Dto
{
    [AutoMapFrom(typeof(Client))]
    [AutoMapTo(typeof(Client))]
    public class ClientDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string ClientCode { get; set; }

        public Guid? KAMResourceId { get; set; }
        public Guid? PMOId { get; set; }

        public ResourceDto KAMResource { get; set; }
        public ResourceDto PMOResource { get; set; }
    }
}
