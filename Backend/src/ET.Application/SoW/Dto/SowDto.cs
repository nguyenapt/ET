using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Beneficiarys.Dto;
using ET.Entities;
using ET.Projects.Dto;
using ET.SOWRoles.Dto;
using ET.SowStatusNotes.Dto;

namespace ET.SoW.Dto
{
    [AutoMapFrom(typeof(SOW))]
    [AutoMapTo(typeof(SOW))]
    public class SowDto : FullAuditedEntityDto<Guid>
    {
        public Guid? ProjectId { get; set; }
        public string Name { get; set; }
        public string FileUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public string ClientPONumber { get; set; }
        public string Description { get; set; }
        public int? SowNumber { get; set; }
        public string Version { get; set; }
        public Guid? BeneficiaryId { get; set; }
        public string InvoicingCycle { get; set; }
        public DateTime? FirstInvoiceDate { get; set; }

        public virtual ProjectDto Project { get; set; }
        public virtual BeneficiaryDto Beneficiary { get; set; }
        public virtual List<SOWRoleDto> SOWRoles { get; set; }
        public virtual List<CreateSowStatusNote> SowStatusNotes { get; set; }
    }
}
