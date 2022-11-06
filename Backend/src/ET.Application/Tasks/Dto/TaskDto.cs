using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.Tasks.Dto
{
    [AutoMapFrom(typeof(Task))]
    [AutoMapTo(typeof(Task))]
    public class TaskDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public Guid? TaskCategoryId { get; set; }
    }
}
