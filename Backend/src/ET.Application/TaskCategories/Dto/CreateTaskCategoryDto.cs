using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.TaskCategorys.Dto
{
    [AutoMapTo(typeof(TaskCategory))]
    public class CreateTaskCategoryDto
    {
        public string Name { get; set; }
    }
}
