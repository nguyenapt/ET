using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.Allocations.Dto
{
    [AutoMapTo(typeof(Allocation))]
    public class UpdateAllocationDto : EntityDto<Guid>, IAllocationDto
    {
        public bool IsBillable { get; set; }

        public Guid SOWRoleId { get; set; }

        public Guid ResourceId { get; set; }
        public string RateType { get; set; }

        public double? FTE { get; set; }

        public double? TotalHours { get; set; }

        public double? TotalHoursPerMonth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; }
        public string TimeNote { get; set; }
        public int? AllocationStatusId { get; set; }
        
        public int? AllocationTypeId { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Estimated hours per week should not be less than 0")]
        public double? EstHoursPerWeek { get; set; }
        public DateTime? ForecastTime { get; set; }
    }
}
