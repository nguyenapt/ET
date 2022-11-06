using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using ET.Allocations;
using ET.Authorization;
using ET.Entities;
using ET.Helper;
using ET.InternalTypes;
using ET.SOWRoles.Dto;
using Task = System.Threading.Tasks.Task;

namespace ET.SOWRoles
{
    [AbpAuthorize(PermissionNames.Pages_SoWs)]
    public class SOWRoleAppService : AsyncCrudAppService<SOWRole, SOWRoleDto, Guid, SOWRoleResultRequestDto, CreateSOWRoleDto, UpdateSowRoleDto>, ISOWRoleAppService
    {
        private readonly IForecastCalculator _forecastCalculator;
        private readonly IRepository<SowRoleTimeStamp, Guid> _sowRoleTimeStampRepository;
        private readonly SOWRoleRepository _sowRoleRepository;

        public SOWRoleAppService(IRepository<SOWRole, Guid> repository,
            IForecastCalculator forecastCalculator,
            IRepository<SowRoleTimeStamp, Guid> sowRoleTimeStampRepository,
            SOWRoleRepository sowRoleRepository) : base(repository)
        {
            _forecastCalculator = forecastCalculator;
            _sowRoleTimeStampRepository = sowRoleTimeStampRepository;
            _sowRoleRepository = sowRoleRepository;
        }

        public override Task<SOWRoleDto> UpdateAsync(UpdateSowRoleDto input)
        {
            var sowRole = base.UpdateAsync(input);
            if (sowRole.Result != null)
            {
                UpsertForecastSowRole(sowRole.Result);
            }
            return sowRole;
        }

        public override Task<SOWRoleDto> GetAsync(EntityDto<Guid> input)
        {
            var sowModel = base.GetAsync(input);
            if (sowModel?.Result == null) return null;
            sowModel.Result.TotalHours = sowModel.Result.TotalHours.RoundNumber();
            sowModel.Result.TotalHoursPerMonth = sowModel.Result.TotalHoursPerMonth.RoundNumber();
            return sowModel;
        }

        public Task<List<SOWRoleDto>> GetSupportersBySowIdAsync(SowRolesRequestDto request)
        {
            var result = _sowRoleRepository.GetSowRolesWithSupporterType(request);
            return result;
        }

        private void UpsertForecastSowRole(SOWRoleDto sowRoleResult)
        {
            if (!sowRoleResult.EstHoursPerWeek.HasValue) return;
            if (!sowRoleResult.ForecastTime.HasValue)
            {
                sowRoleResult.ForecastTime = DateTime.Now.AddMonths(1);
            }
            var firstDayOfMonth = new DateTime(sowRoleResult.ForecastTime.Value.Year, sowRoleResult.ForecastTime.Value.Month, 1);
            var startDate = sowRoleResult.StartDate >= firstDayOfMonth ? sowRoleResult.StartDate : firstDayOfMonth;
            
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var endDate = sowRoleResult.EndDate.HasValue && sowRoleResult.EndDate.Value >= lastDayOfMonth ? sowRoleResult.EndDate.Value : lastDayOfMonth;
            
            var existedTimeStamp = _sowRoleTimeStampRepository
                                       .GetAll().FirstOrDefault(x => x.SowRoleId == sowRoleResult.Id && x.StartDate == firstDayOfMonth && x.EndDate == endDate) ??
                                   new SowRoleTimeStamp();

            existedTimeStamp.SowRoleId = sowRoleResult.Id;
            existedTimeStamp.StartDate = startDate;
            existedTimeStamp.EndDate = endDate;
            existedTimeStamp.EstHoursPerWeek = sowRoleResult.EstHoursPerWeek.Value;
            existedTimeStamp.ActualRate = sowRoleResult.ActualRate;
            existedTimeStamp.Estimate = _forecastCalculator.Calculate(startDate, endDate,
                sowRoleResult.EstHoursPerWeek.Value, sowRoleResult.ActualRate);

            _sowRoleTimeStampRepository.InsertOrUpdate(existedTimeStamp);
        }

        public async Task<SoWRoleDetailListDto> GetSoWRoleDetailList(Guid sowId)
        {
            var sowRolelist = await _sowRoleRepository.GetNonSupporterSowRoles(new SowRolesRequestDto(sowId));
            var result = new SoWRoleDetailListDto
            {
                Totals = new SoWRoleFeeTotalDto(),
                Items = new List<SoWRoleDetailItemDto>()
            };

            // Calculate totals
            var groupByCurrency = sowRolelist.GroupBy(x => x.Currency);

            result.Totals.FixRateCardFee = CreateSoWRoleFeeDtoTotals(groupByCurrency, x => x.StandardRate,true);
            result.Totals.FixForcastFee = CreateSoWRoleFeeDtoTotals(groupByCurrency, x => x.ActualRate,true);
            result.Totals.MonthlyRateCardFee = CreateSoWRoleFeeDtoTotals(groupByCurrency, x => x.StandardRate,false);
            result.Totals.MonthlyForcastFee = CreateSoWRoleFeeDtoTotals(groupByCurrency, x => x.ActualRate,false);

            // Add sow dtos
            foreach (var item in sowRolelist)
            {
                var itemDto = ObjectMapper.Map<SoWRoleDetailItemDto>(item);
                itemDto.TotalHours = itemDto.TotalHours.RoundNumber();
                itemDto.TotalHoursPerMonth = itemDto.TotalHoursPerMonth.RoundNumber();
                
                result.Items.Add(itemDto);
                itemDto.FixRateCardFee = result.Totals.FixRateCardFee.Select(x => CreateSoWRoleFeeDto(item, x.Currency, item.StandardRate,true)).ToList();
                itemDto.FixForcastFee = result.Totals.FixRateCardFee.Select(x => CreateSoWRoleFeeDto(item, x.Currency, item.ActualRate,true)).ToList();
                itemDto.MonthlyRateCardFee = result.Totals.MonthlyRateCardFee.Select(x => CreateSoWRoleFeeDto(item, x.Currency, item.StandardRate,false)).ToList();
                itemDto.MonthlyForcastFee = result.Totals.MonthlyForcastFee.Select(x => CreateSoWRoleFeeDto(item, x.Currency, item.ActualRate,false)).ToList();
            }

            return await Task.FromResult(result);
        }

        private List<SoWRoleFeeDto> CreateSoWRoleFeeDtoTotals(IEnumerable<IGrouping<string, SOWRole>> groups, Func<SOWRole, double> rateSelector, bool isFixFee)
        {
            return groups.Select(x => new SoWRoleFeeDto
            {
                Currency = x.Key,
                Fee = x.Sum(r =>
                {
                    if (isFixFee)
                    {
                        if (r.BillingType.EndsWith("T") || r.BillingType.EndsWith("TB"))
                        {
                            return r.BillingType.StartsWith("TMPT")
                                ? rateSelector(r) * r.TotalHours.GetValueOrDefault(1)
                                : rateSelector(r) * r.FTE.GetValueOrDefault(1) * r.TotalHours.GetValueOrDefault(1);
                        }
                        return null;
                    }
                    if (r.BillingType.EndsWith("C") || r.BillingType.EndsWith("CB"))
                    {
                        return r.BillingType.StartsWith("TMPT")
                            ? rateSelector(r) * r.TotalHoursPerMonth.GetValueOrDefault(1)
                            : rateSelector(r) * r.FTE.GetValueOrDefault(1) * r.TotalHoursPerMonth.GetValueOrDefault(1);
                    }
                    return null;
                })
            })
                .OrderBy(x => x.Currency)
                .ToList();
        }
        
        private SoWRoleFeeDto CreateSoWRoleFeeDto(SOWRole item, string currency, double rate, bool isFixFee)
        {
            var dto = new SoWRoleFeeDto { Currency = currency, Fee = 0 };
            if (dto.Currency != item.Currency) return dto;

            if (isFixFee)
            {
                if (item.BillingType.EndsWith("T") || item.BillingType.EndsWith("TB"))
                {
                    dto.Fee = item.BillingType.StartsWith("TMPT")
                        ? rate * item.TotalHours.GetValueOrDefault(1)
                        : rate * item.FTE.GetValueOrDefault(1) * item.TotalHours.GetValueOrDefault(1);
                }
                return dto;
            }

            if (item.BillingType.EndsWith("C") || item.BillingType.EndsWith("CB"))
            {
                dto.Fee = item.BillingType.StartsWith("TMPT")
                    ? rate * item.TotalHoursPerMonth.GetValueOrDefault(1)
                    : rate * item.FTE.GetValueOrDefault(1) * item.TotalHoursPerMonth.GetValueOrDefault(1);
            }
            return dto;
        }
    }
}

