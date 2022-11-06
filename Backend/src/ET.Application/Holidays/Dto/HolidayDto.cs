using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.Holidays.Dto
{
    [AutoMapFrom(typeof(Holiday))]
    [AutoMapTo(typeof(Holiday))]
    public class HolidayDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
