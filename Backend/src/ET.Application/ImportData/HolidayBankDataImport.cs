using System;
using Abp.Domain.Repositories;
using ET.Entities;
using ET.ImportData.Dto;

namespace ET.ImportData
{
    public class HolidayBankDataImport : ImportDataBaseAppService<Holiday, Guid, HolidayBankDto>
    {
        public HolidayBankDataImport(IRepository<Holiday, Guid> repository) : base(repository)
        {
        }
    }
}
