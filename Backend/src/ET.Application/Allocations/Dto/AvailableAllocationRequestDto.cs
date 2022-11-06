using System.ComponentModel.DataAnnotations;

namespace ET.Allocations.Dto
{
    public class AvailableAllocationRequestDto
    {
        [Required]
        public string UserName { get; set; }
    }
}
