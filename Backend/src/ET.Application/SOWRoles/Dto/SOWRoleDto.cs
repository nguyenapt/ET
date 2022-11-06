using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;
using ET.InternalTypes.Dto;
using ET.SoW.Dto;

namespace ET.SOWRoles.Dto
{
    [AutoMapFrom(typeof(SOWRole))]
    [AutoMapTo(typeof(SOWRole))]
    public class SOWRoleDto : FullAuditedEntityDto<Guid>
    {
        public Guid SOWId { get; set; }
        public bool IsBillable { get; set; }
        public string BillingType { get; set; }
        public string RoleName { get; set; }
        public string RateType { get; set; }
        public string Currency { get; set; }
        public double StandardRate { get; set; }
        public double ActualRate { get; set; }
        public double? FTE { get; set; }
        public double? TotalHours { get; set; }
        public double? TotalHoursPerMonth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Term { get; set; }
        public string Description { get; set; }
        public string TimeNote { get; set; }
        public double? EstHoursPerWeek { get; set; }
        public DateTime? ForecastTime { get; set; }
        public Guid? InternalTypeId { get; set; }

        public SowDto SOW { get; set; }
        public InternalTypeDto InternalType { get; set; }
    }
}
