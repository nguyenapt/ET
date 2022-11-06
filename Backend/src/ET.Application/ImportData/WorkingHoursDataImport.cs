using System;
using Abp.Domain.Repositories;
using ET.Entities;
using ET.ImportData.Dto;


namespace ET.ImportData
{
    public class WorkingHoursDataImport : ImportDataBaseAppService<WorkingHourRule, Guid, WorkingHoursDto>
    {
        public WorkingHoursDataImport(IRepository<WorkingHourRule, Guid> repository) : base(repository)
        {
        }
    }
}
