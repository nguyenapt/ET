using System;
using Abp.Domain.Repositories;
using ET.Entities;
using ET.ImportData.Dto;

namespace ET.ImportData
{
    public class BeneficiaryInformationDataImport : ImportDataBaseAppService<Beneficiary, Guid, BeneficialInformationDto>
    {
        public BeneficiaryInformationDataImport(IRepository<Beneficiary, Guid> repository) : base(repository)
        {
        }
    }
}
