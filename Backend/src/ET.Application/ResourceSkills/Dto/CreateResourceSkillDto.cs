using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.ResourceSkills.Dto
{
    [AutoMapTo(typeof(ResourceSkill))]
    public class CreateResourceSkillDto
    {
        public string Name { get; set; }
    }
}
