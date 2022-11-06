using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.WorkingHourRules.Dto
{
    [AutoMapFrom(typeof(WorkingHourRule))]
    [AutoMapTo(typeof(WorkingHourRule))]
    public class WorkingHourRuleDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
