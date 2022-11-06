using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.Beneficiarys.Dto
{
    [AutoMapTo(typeof(Beneficiary))]
    public class CreateBeneficiaryDto
    {
        public string Name { get; set; }
    }
}
