using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;
using ET.SOWRoles.SowRoleValidators;

namespace ET.SoW.Dto
{
    [AutoMapTo(typeof(SOW))]
    public class UpdateSowDto : FullAuditedEntityDto<Guid>, ICustomValidate
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
        [Range(0, double.MaxValue, ErrorMessage = "Version must be greater than zero.")]
        public decimal? Version { get; set; }
        public Guid? BeneficiaryId { get; set; }
        public string StatusNote { get; set; }

        [StringLength(50)]
        public string InvoicingCycle { get; set; }

        public DateTime? FirstInvoiceDate { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (Status != AppEnums.SowStatus.Draft.ToString() && string.IsNullOrEmpty(StatusNote))
            {
                context.Results.Add(new ValidationResult(SowValidateMessage.StatusNoteRequired, new[] { "StatusNote" }));
            }

            if (!string.IsNullOrWhiteSpace(InvoicingCycle)
                && !(InvoicingCycle == AppEnums.EInvoicingCycle.Monthly.ToString()
                     || InvoicingCycle == AppEnums.EInvoicingCycle.Quarterly.ToString()))
            {
                context.Results.Add(new ValidationResult(SowValidateMessage.InvoicingCycle, new[] { "InvoicingCycle" }));
            }
        }
    }
}
