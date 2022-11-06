using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.Beneficiarys.Dto
{
    [AutoMapFrom(typeof(Beneficiary))]
    [AutoMapTo(typeof(Beneficiary))]
    public class BeneficiaryDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
