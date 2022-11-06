using System;
using Abp.Application.Services;
using ET.Holidays.Dto;
using ET.SoW.Dto;

namespace ET.Holidays
{
    public interface IHolidayAppService : IAsyncCrudAppService<HolidayDto, Guid, HolidayResultRequestDto, CreateHolidayDto, HolidayDto>
    {
    }
}


