using ET.Allocations.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ET.Allocations.AllocationValidators
{
    public interface IAllocationValidator
    {
        Task<List<ValidationResult>> Validate(IAllocationDto dto);
    }
}
