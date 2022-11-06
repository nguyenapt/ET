using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.Holidays.Dto
{
    [AutoMapTo(typeof(Holiday))]
    public class CreateHolidayDto
    {
        public string Name { get; set; }
    }
}
