using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.SkillLevels.Dto
{
    [AutoMapTo(typeof(SkillLevel))]
    public class CreateSkillLevelDto
    {
        public string Level { get; set; }
        public string Description { get; set; }
    }
}
