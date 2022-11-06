using Abp.AutoMapper;
using ET.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ET.SOWRoles.Dto
{
    public class SoWRoleDetailListDto
    {
        public List<SoWRoleDetailItemDto> Items { get; set; }
        public SoWRoleFeeTotalDto Totals { get; set; }

    }

    [AutoMapFrom(typeof(SOWRole))]
    public class SoWRoleDetailItemDto
    {
        public Guid Id { get; set; }
        [DisplayName("Bill")]
        public bool IsBillable { get; set; }

        [DisplayName("Billing Type")]
        public string BillingType { get; set; }

        [DisplayName("Role Name")]
        public string RoleName { get; set; }

        [DisplayName("Rate Type")]
        public string RateType { get; set; }

        [DisplayName("Currency")]
        public string Currency { get; set; }

        [DisplayName("Rate Card")]
        public double StandardRate { get; set; }

        [DisplayName("Actual")]
        public double ActualRate { get; set; }

        [DisplayName("FTE")]
        public double? FTE { get; set; }

        [DisplayName("Total")]
        public double? TotalHours { get; set; }

        [DisplayName("Monthly")]
        public double? TotalHoursPerMonth { get; set; }

        [DisplayName("Start")]
        public DateTime? StartDate { get; set; }

        [DisplayName("End")]
        public DateTime? EndDate { get; set; }

        [DisplayName("InternalTypeId")]
        public Guid? InternalTypeId { get; set; }


        [DisplayName("Term")]
        public int? Term { get; set; }


        [DisplayName("Description")]
        public string Description { get; set; }

        public List<SoWRoleFeeDto> FixRateCardFee { get; set; }
        public List<SoWRoleFeeDto> FixForcastFee { get; set; }
        public List<SoWRoleFeeDto> MonthlyRateCardFee { get; set; }
        public List<SoWRoleFeeDto> MonthlyForcastFee { get; set; }
    }

    public class SoWRoleFeeDto
    {
        public string Currency { get; set; }
        public double? Fee { get; set; }
        public bool IsActive { get; set; }
    }

    public class SoWRoleFeeTotalDto
    {
        public List<SoWRoleFeeDto> FixRateCardFee { get; set; }
        public List<SoWRoleFeeDto> FixForcastFee { get; set; }
        public List<SoWRoleFeeDto> MonthlyRateCardFee { get; set; }
        public List<SoWRoleFeeDto> MonthlyForcastFee { get; set; }
    }
}
