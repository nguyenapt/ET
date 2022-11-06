using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using ET.Entities;
using ET.SOWRoles.Dto;
using ET.SOWRoles.SowRoleValidators;

namespace ET.SoW.Dto
{
    [AutoMapTo(typeof(SOW))]
    [AutoMapFrom(typeof(SOW))]
    public class CreateSowDto : IShouldNormalize, ICustomValidate
    {
        public Guid ProjectId { get; set; }
        [Required]
        public string Name { get; set; }
        public string FileUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public string ClientPONumber { get; set; }
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "SowNumber must be greater than zero.")]
        public int? SowNumber { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Version must be greater or equal to zero.")]
        public decimal? Version { get; set; }

        [StringLength(50)]
        public string InvoicingCycle { get; set; }
        public DateTime? FirstInvoiceDate { get; set; }

        public Guid? BeneficiaryId { get; set; }
        public List<CreateSOWRoleDto> SOWRoles { get; set; }

        public void Normalize()
        {
            Status = AppEnums.SowStatus.Draft.ToString();
            Name = string.IsNullOrWhiteSpace(Name) ? Name : Name.Trim();
            if (FirstInvoiceDate.HasValue) return;

            var nextMonth = DateTime.Today.AddMonths(1);
            var firstDayOfNextMonth = new DateTime(nextMonth.Year, nextMonth.Month, 1);
            FirstInvoiceDate = firstDayOfNextMonth;
        }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (!string.IsNullOrWhiteSpace(InvoicingCycle)
                && !(InvoicingCycle == AppEnums.EInvoicingCycle.Monthly.ToString()
                    || InvoicingCycle == AppEnums.EInvoicingCycle.Quarterly.ToString()))
            {
                context.Results.Add(new ValidationResult(SowValidateMessage.InvoicingCycle, new[] { "InvoicingCycle" }));
            }
        }
    }
}
