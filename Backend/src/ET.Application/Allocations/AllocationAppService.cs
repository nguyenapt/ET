using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using ET.Allocations.AllocationValidators;
using ET.Allocations.Dto;
using ET.AllocationTypes;
using ET.AllocationTypes.Dto;
using ET.Email.Dto;
using ET.Email.Helper;
using ET.Entities;
using ET.Helper;
using ET.InternalTypes;
using ET.Resources;
using ET.Resources.Dto;
using ET.Sessions;
using ET.Shared.Dto;
using ET.SOWRoles.Exceptions;
using ET.Users.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ET.Allocations
{
    public class AllocationsAppService : AsyncCrudAppService<Allocation, AllocationDto, Guid, AllocationResultRequestDto, CreateAllocationDto, UpdateAllocationDto>, IAllocationsAppService
    {
        private readonly AllocationRepository _allocationRepository;
        private readonly IForecastCalculator _forecastCalculator;
        private readonly IRepository<AllocationTimeStamp, Guid> _allocationTimeStampRepository;
        private readonly IETEmailHelper _emailHelper;
        private readonly IAllocationValidator _allocationValidator;
        private readonly SessionAppService _sessionAppService;
        private readonly IRepository<AllocationStatus, int> _allocationStatusRepository;
        private readonly IRepository<AllocationType, int> _allocationTypeRepository;
        private readonly IRepository<SOWRole, Guid> _sowRoleRepository;
        private readonly InternalTypeRepository _internalTypeRepository;
        private readonly IRepository<Resource, Guid> _resourceRepository;
        public AllocationsAppService(
            IRepository<Allocation, Guid> repository,
            AllocationRepository allocationRepository,
            IForecastCalculator forecastCalculator,
            IRepository<AllocationTimeStamp, Guid> allocationTimestampRepository,
            IETEmailHelper emailHelper,
            IAllocationValidator allocationValidator,
            IRepository<AllocationStatus, int> allocationStatusRepository,
            SessionAppService sessionAppService,
            IRepository<AllocationType, int> allocationTypeRepository, IRepository<SOWRole, Guid> sowRoleRepository, InternalTypeRepository internalTypeRepository, IRepository<Resource, Guid> resourceRepository) : base(repository)
        {
            _allocationRepository = allocationRepository;
            _forecastCalculator = forecastCalculator;
            _allocationTimeStampRepository = allocationTimestampRepository;
            _emailHelper = emailHelper;
            _allocationValidator = allocationValidator;
            _sessionAppService = sessionAppService;
            _allocationStatusRepository = allocationStatusRepository;
            _allocationTypeRepository = allocationTypeRepository;
            _sowRoleRepository = sowRoleRepository;
            _internalTypeRepository = internalTypeRepository;
            _resourceRepository = resourceRepository;
        }

        protected override AllocationDto MapToEntityDto(Allocation entity)
        {
            var fullEntity = _allocationRepository.GetAllIncluding(
                    x => x.SOWRole.SOW.Project.ProjectManager.User,
                    x => x.Resource.User, x => x.AllocationType)
                    .FirstOrDefault(x => x.Id == entity.Id);
            return base.MapToEntityDto(fullEntity);
        }

        public IEnumerable<ResourceDto> GetCurrentManagers()
        {
            var loginInformation = _sessionAppService.GetCurrentLoginInformations().Result;
            if (loginInformation?.User == null)
            {
                throw new UserFriendlyException(404, "Cannot find current user");
            }

            var allResources = _resourceRepository.GetAll();
            var resource = allResources.FirstOrDefault(x => x.UserId == loginInformation.User.Id);
            var dm = allResources.FirstOrDefault(x => x.DepartmentId == resource.DepartmentId && x.JobTitle == "DM");

            var allocations = Repository.GetAllIncluding(x => x.Resource).Where(x => x.Resource.UserId == loginInformation.User.Id);
            var projects = allocations.Select(x => x.SOWRole.SOW.Project).Where(x => x.ProjectManager != null);
            var projectsManagers = projects.Select(x => x.ProjectManager).Distinct().ToList();
            if (dm != null) projectsManagers.Insert(0, dm);

            return projectsManagers.Select(x => ObjectMapper.Map<ResourceDto>(x));
        }

        public override async Task<AllocationDto> CreateAsync(CreateAllocationDto input)
        {
            var validationResult = await _allocationValidator.Validate(input);
            if (validationResult.Any())
            {
                var errorMessages = validationResult.Select(x => x.ErrorMessage).ToList();
                var message = string.Join(", ", errorMessages);
                throw new UserFriendlyException(404, message);
            }

            var sowRole = await _sowRoleRepository.GetAsync(input.SOWRoleId);
            if (sowRole == null)
            {
                throw new SowRoleNotFoundException($"Cannot find sow role with Id: {input.SOWRoleId}");
            }

            var isInternalTypeAvailable = await _internalTypeRepository.IsInternalTypeSupporter(new IsInternalTypeSupporterRequestDto(sowRole.InternalTypeId));

            if (!isInternalTypeAvailable)
            {
                if (input.IsBillable)
                {
                    //input.AllocationType = AllocationConstant.TypeBill;
                    var typeBill = await _allocationTypeRepository.GetAll().FirstOrDefaultAsync(x => x.Name == AllocationConstant.TypeBill);
                    if (typeBill != null)
                    {
                        input.AllocationTypeId = typeBill.Id;
                    }
                }
                else
                {
                    var typeNonBill = await _allocationTypeRepository.GetAll().FirstOrDefaultAsync(x => x.Name == AllocationConstant.TypeNonBill);
                    if (typeNonBill != null)
                    {
                        input.AllocationTypeId = typeNonBill.Id;
                    }
                }
            }

            input.IsActive = true;

            var allocationDto = base.CreateAsync(input);

            //Send email to notify a resource about their Allocation
            if (allocationDto.Result?.Resource?.User != null)
                NotifyAllocatedResource(allocationDto.Result);

            return allocationDto?.Result;
        }

        private void NotifyAllocatedResource(AllocationDto allocationDtoResult)
        {
            var project = allocationDtoResult.SOWRole?.SOW?.Project;

            var allocatedResourceEmailDto = new AllocatedResourceEmailDto
            {
                FullName = $"{allocationDtoResult.Resource.FirstName} {allocationDtoResult.Resource.LastName}",
                PMEmail = project?.ProjectManager?.User?.EmailAddress ?? string.Empty,
                ProjectName = project?.Name ?? string.Empty,
                FTE = allocationDtoResult.FTE?.ToString(CultureInfo.InvariantCulture) ?? string.Empty,
                EndDate = allocationDtoResult.EndDate.HasValue
                    ? allocationDtoResult.EndDate.Value.ToString("dd/MM/yyyy")
                    : string.Empty,
                StartDate = allocationDtoResult.StartDate.ToString("dd/MM/yyyy"),
                Role = allocationDtoResult.SOWRole?.RoleName ?? string.Empty,
                BillType = allocationDtoResult.SOWRole?.BillingType ?? string.Empty,
                PMName = $"{project?.ProjectManager?.FirstName ?? string.Empty} {project?.ProjectManager?.LastName ?? string.Empty}",
                TimeNote = allocationDtoResult.TimeNote,
                TotalHour = allocationDtoResult.TotalHours.ToString(),
                TotalHourPerMonth = allocationDtoResult.TotalHoursPerMonth.ToString()
            };

            _emailHelper.SendMailGenericAsync(ETConsts.ETEmailSettings.AllocatedResourceName,
            new List<string> { $"{allocationDtoResult.Resource.User.EmailAddress}" },
            null,
            null, allocatedResourceEmailDto,
            null);
        }

        public IEnumerable<AllocationTypeDto> GetAvailableAllocationTypeForSupporters()
        {
            var allocationTypes = _allocationTypeRepository.GetAll().Where(x => x.IsSupporter.HasValue && x.IsSupporter.Value);
            return ObjectMapper.Map<IEnumerable<AllocationTypeDto>>(allocationTypes);
        }

        public override async Task<AllocationDto> UpdateAsync(UpdateAllocationDto input)
        {
            var validationResult = await _allocationValidator.Validate(input);
            if (validationResult.Any())
            {
                var errorMessages = validationResult.Select(x => x.ErrorMessage).ToList();
                var message = string.Join(", ", errorMessages);
                throw new UserFriendlyException(404, message);
            }

            var sowRole = await _sowRoleRepository.GetAsync(input.SOWRoleId);
            if (sowRole == null)
            {
                throw new SowRoleNotFoundException($"Cannot find sow role with Id: {input.SOWRoleId}");
            }

            var isInternalTypeAvailable = await _internalTypeRepository.IsInternalTypeSupporter(new IsInternalTypeSupporterRequestDto(sowRole.InternalTypeId));

            if (!isInternalTypeAvailable)
            {
                if (input.IsBillable)
                {
                    var typeBill = await _allocationTypeRepository.GetAll().FirstOrDefaultAsync(x => x.Name == AllocationConstant.TypeBill);
                    if (typeBill != null)
                    {
                        input.AllocationTypeId = typeBill.Id;
                    }
                }
                else
                {
                    var typeNonBill = await _allocationTypeRepository.GetAll().FirstOrDefaultAsync(x => x.Name == AllocationConstant.TypeNonBill);
                    if (typeNonBill != null)
                    {
                        input.AllocationTypeId = typeNonBill.Id;
                    }
                }
            }

            input.IsActive = true;

            var allocation = base.UpdateAsync(input);
            if (allocation.Result != null)
                UpsertForecastAllocation(allocation.Result);

            return allocation?.Result;
        }

        public async Task<List<AvailableAllocationForUserResponseDto>> GetAvailableAllocationsForCurrentUser()
        {
            var loginInformation = await _sessionAppService.GetCurrentLoginInformations();
            if (loginInformation?.User == null)
            {
                throw new UserNotFoundException("Cannot find current user");
            }

            var userRequest = new AvailableAllocationForUserRequestDto()
            {
                UserId = loginInformation.User.Id,
                UserName = loginInformation.User.UserName
            };

            var availableAllocations = await _allocationRepository.GetAvailableAllocationsForUser(userRequest);
            return availableAllocations;
        }

        private void UpsertForecastAllocation(AllocationDto allocationResult)
        {
            if (!allocationResult.EstHoursPerWeek.HasValue) return;
            if (!allocationResult.ForecastTime.HasValue)
            {
                allocationResult.ForecastTime = DateTime.Now.AddMonths(1);
            }

            var firstDayOfMonth = new DateTime(allocationResult.ForecastTime.Value.Year, allocationResult.ForecastTime.Value.Month, 1);
            var startDate = allocationResult.StartDate >= firstDayOfMonth ? allocationResult.StartDate : firstDayOfMonth;

            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var endDate = allocationResult.EndDate.HasValue && allocationResult.EndDate.Value >= lastDayOfMonth ? allocationResult.EndDate.Value : lastDayOfMonth;

            var existedTimeStamp = _allocationTimeStampRepository
                                       .GetAll().FirstOrDefault(x => x.AllocationId == allocationResult.Id && x.StartDate == startDate && x.EndDate == endDate) ??
                                   new AllocationTimeStamp();

            existedTimeStamp.AllocationId = allocationResult.Id;
            existedTimeStamp.StartDate = startDate;
            existedTimeStamp.EndDate = endDate;
            existedTimeStamp.EstHoursPerWeek = allocationResult.EstHoursPerWeek.Value;
            existedTimeStamp.ActualRate = allocationResult.SOWRole.ActualRate;
            existedTimeStamp.Estimate = _forecastCalculator.Calculate(startDate, endDate,
                allocationResult.EstHoursPerWeek.Value, allocationResult.SOWRole.ActualRate);

            _allocationTimeStampRepository.InsertOrUpdate(existedTimeStamp);
        }

        public async Task<List<AvailableResourceDto>> GetAvailableResourcesAsync(AllocationResultRequestDto request)
        {
            var result = await _allocationRepository.GetAvailableResources(request);
            foreach (var availableResource in result)
            {
                availableResource.TotalHours = availableResource.TotalHours.RoundNumber();
                availableResource.TotalHoursPerMonth = availableResource.TotalHoursPerMonth.RoundNumber();
            }

            return result;
        }
        public async Task<List<SOWRolesAllocationStatusDto>> GetSOWRolesAllocationStatusAsync(SOWRolesAllocationStatusRequestDto request)
        {
            var result = await _allocationRepository.GetSOWRolesAllocationStatus(request);
            foreach (var sowRoleAllocation in result)
            {
                sowRoleAllocation.TotalHours = sowRoleAllocation.TotalHours.RoundNumber();
                sowRoleAllocation.TotalHoursPerMonth = sowRoleAllocation.TotalHoursPerMonth.RoundNumber();
            }

            return result;
        }
        public async Task<List<AllocationForResourceDto>> GetAllocationDetailForResourceAsync(AllocationForResourceRequestDto request)
        {
            return await _allocationRepository.GetAllocationDetailForResource(request);
        }
        public async Task<List<SOWAllocationStatusDto>> GetSOWAllocationStatusAsync(SOWAllocationStatusRequestDto request)
        {
            return await _allocationRepository.GetSOWAllocationStatus(request);
        }
        public async Task<List<SowRoleAllocationDto>> GetAllocationsForSOWRoleAsync(Guid sowRoleId)
        {
            var result = await _allocationRepository.GetAllocationsForSOWRole(sowRoleId);
            //var allocationStatuses = GetAllAllocationStatus();
            foreach (var sowRoleAllocation in result)
            {
                sowRoleAllocation.TotalHours = sowRoleAllocation.TotalHours.RoundNumber();
                sowRoleAllocation.TotalHoursPerMonth = sowRoleAllocation.TotalHoursPerMonth.RoundNumber();
                //if (sowRoleAllocation.AllocationStatusId.HasValue)
                //{
                //    sowRoleAllocation.AllocationStatusName = allocationStatuses
                //        .FirstOrDefault(x => x.Id.Equals(sowRoleAllocation.AllocationStatusId)).Name;
                //}
            }

            return result;
        }

        //private static bool IsAllocationTypeSupporter(string allocationType)
        //{
        //    if (string.IsNullOrWhiteSpace(allocationType))
        //    {
        //        return false;
        //    }

        //    if (allocationType.Equals(AllocationConstant.TypeDevelopementImprovement, StringComparison.InvariantCultureIgnoreCase)
        //        || allocationType.Equals(AllocationConstant.TypeTraining, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public List<AllocationStatus> GetAllAllocationStatus()
        {
            var result = _allocationStatusRepository.GetAll().Select(x => new AllocationStatus()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            });

            return result.ToList();
        }
    }
}
