using System;
using Abp.Application.Services;
using ET.WorkingHourRules.Dto;
using ET.SoW.Dto;

namespace ET.WorkingHourRules
{
    public interface IWorkingHourRuleAppService : IAsyncCrudAppService<WorkingHourRuleDto, Guid, WorkingHourRuleResultRequestDto, CreateWorkingHourRuleDto, WorkingHourRuleDto>
    {
    }
}


