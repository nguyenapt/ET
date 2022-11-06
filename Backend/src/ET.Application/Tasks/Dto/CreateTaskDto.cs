using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.Tasks.Dto
{
    [AutoMapTo(typeof(Task))]
    public class CreateTaskDto
    {
        public string Name { get; set; }

        public Guid? TaskCategoryId { get; set; }
    }
}
