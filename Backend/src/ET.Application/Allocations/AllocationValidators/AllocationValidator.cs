using Abp.Dependency;
using ET.Allocations.Dto;
using ET.InternalTypes;
using ET.SOWRoles;
using ET.SOWRoles.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ET.Allocations.AllocationValidators
{
    public class AllocationValidator : ITransientDependency, IAllocationValidator
    {
        private readonly InternalTypeRepository _internalTypeRepository;
        private readonly SOWRoleRepository _sowRoleRepository;

        public AllocationValidator(InternalTypeRepository internalTypeRepository, SOWRoleRepository sowRoleRepository)
        {
            _internalTypeRepository = internalTypeRepository;
            _sowRoleRepository = sowRoleRepository;
        }

        public async Task<List<ValidationResult>> Validate(IAllocationDto dto)
        {
            var validationResult = new List<ValidationResult>();
            var sowRole = await _sowRoleRepository.GetAsync(dto.SOWRoleId);
            if (sowRole == null)
            {
                throw new SowRoleNotFoundException($"Cannot find sow role with Id: {dto.SOWRoleId}");
            }

            var isInternalTypeAvailable = await _internalTypeRepository.IsInternalTypeSupporter
                (new IsInternalTypeSupporterRequestDto(sowRole.InternalTypeId));
            
            dto.IsBillable = sowRole.IsBillable;
            //var isAllocationTypeContainedInSupporterRoles = AllocationTypesInSupporterSowRoles
            //    .FindIndex(x => x.Equals(dto.AllocationType, StringComparison.OrdinalIgnoreCase)) != -1;
            //var isAllocationTypeContainedInNonSupporterRoles = AllocationTypesNotInSupporterSowRoles
            //.FindIndex(x => x.Equals(dto.AllocationType, StringComparison.OrdinalIgnoreCase)) != -1;

            //if (isInternalTypeAvailable && isAllocationTypeContainedInNonSupporterRoles)
            //{
            //    validationResult.Add(new ValidationResult($"Cannot add {dto.AllocationType} as Allocation Type for Bill and Non bill Sow Role"));
            //}

            if (dto.TotalHours < 0)
            {
                validationResult.Add(new ValidationResult("Total hours should be greater than 0"));
            }

            if (dto.TotalHoursPerMonth < 0)
            {
                validationResult.Add(new ValidationResult("Total hours per month should be greater than 0"));
            }

            //if (!isInternalTypeAvailable && isAllocationTypeContainedInSupporterRoles && !dto.IsBillable)
            //{
            //    validationResult.Add(new ValidationResult($"Cannot add {dto.AllocationType} as Allocation Type for Supporter Sow Role"));
            //}

            //if (dto.IsBillable && !string.IsNullOrWhiteSpace(dto.AllocationType) && dto.AllocationType.Equals("Non-Bill", StringComparison.InvariantCultureIgnoreCase))
            //{
            //    validationResult.Add(new ValidationResult($"Cannot add {dto.AllocationType} as Allocation Type for Billable sow role"));
            //}

            //if (!dto.IsBillable && !string.IsNullOrWhiteSpace(dto.AllocationType) && dto.AllocationType.Equals("Bill", StringComparison.InvariantCultureIgnoreCase))
            //{
            //    validationResult.Add(new ValidationResult($"Cannot add {dto.AllocationType} as Allocation Type for Non-Billable sow role"));
            //}

            return validationResult;
        }

        private static readonly List<string> AllocationTypesInSupporterSowRoles = new List<string>()
        {
                AllocationConstant.TypeDevelopementImprovement, AllocationConstant.TypeTraining
        };

        private static readonly List<string> AllocationTypesNotInSupporterSowRoles = new List<string>()
        {
                AllocationConstant.TypeBill, AllocationConstant.TypeNonBill
        };
    }
}
