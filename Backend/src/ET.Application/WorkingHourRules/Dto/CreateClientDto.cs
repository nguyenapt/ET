using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.WorkingHourRules.Dto
{
    [AutoMapTo(typeof(WorkingHourRule))]
    public class CreateWorkingHourRuleDto
    {
        public string Name { get; set; }
    }
}
