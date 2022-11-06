using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using ET.Entities;
using ET.SOWRoles.SowRoleValidators;
using Microsoft.EntityFrameworkCore.Internal;

namespace ET.SOWRoles.Dto
{
    [AutoMapTo(typeof(SOWRole))]
    public class UpdateSowRoleDto : FullAuditedEntityDto<Guid>, ISowRoleDto, ICustomValidate
    {
        public Guid SOWId { get; set; }
        public bool IsBillable { get; set; }
        public string BillingType { get; set; }
        public string RoleName { get; set; }
        public string RateType { get; set; }
        public string Currency { get; set; }
        [Required]
        public double StandardRate { get; set; }
        [Required]
        public double? ActualRate { get; set; }
        public double? FTE { get; set; }
        public double? TotalHours { get; set; }
        public double? TotalHoursPerMonth { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Term { get; set; }
        public string Description { get; set; }
        public string TimeNote { get; set; }
        public double? EstHoursPerWeek { get; set; }
        public DateTime? ForecastTime { get; set; }
        public Guid? InternalTypeId { get; set; }


        public void AddValidationErrors(CustomValidationContext context)
        {
            var validationResult = SowRoleValidatorHelper.Validate(this);

            if (validationResult.Any())
            {
                context.Results.AddRange(validationResult);
            }
        }
    }
}
