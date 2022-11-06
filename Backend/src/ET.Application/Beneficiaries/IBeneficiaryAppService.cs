using System;
using Abp.Application.Services;
using ET.Beneficiarys.Dto;
using ET.SoW.Dto;

namespace ET.Beneficiarys
{
    public interface IBeneficiaryAppService : IAsyncCrudAppService<BeneficiaryDto, Guid, BeneficiaryResultRequestDto, CreateBeneficiaryDto, BeneficiaryDto>
    {
    }
}


