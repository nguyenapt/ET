using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.AllocationTypes.Dto;
using ET.Entities;
using ET.Resources.Dto;
using ET.SOWRoles.Dto;
using ET.TimesheetEntries.Dto;

namespace ET.Allocations.Dto
{
    [AutoMapFrom(typeof(Allocation))]
    [AutoMapTo(typeof(Allocation))]
    public class AllocationDto : EntityDto<Guid>
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
        public string AllocationTypeName { get; set; }
        public int? AllocationTypeId { get; set; }
        public double? EstHoursPerWeek { get; set; }
        public DateTime? ForecastTime { get; set; }
        public ResourceDto Resource { get; set; }
        public SOWRoleDto SOWRole { get; set; }
        public TimesheetEntryDto TimesheetEntry { get; set; }

        public virtual AllocationTypeDto AllocationType { get; set; }
    }
}
