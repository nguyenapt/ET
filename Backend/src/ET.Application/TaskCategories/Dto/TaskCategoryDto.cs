using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.TaskCategorys.Dto
{
    [AutoMapFrom(typeof(TaskCategory))]
    [AutoMapTo(typeof(TaskCategory))]
    public class TaskCategoryDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string RoleName { get; set; }
    }
}
