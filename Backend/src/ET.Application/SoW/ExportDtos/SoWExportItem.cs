using Abp.AutoMapper;
using ET.Shared.Dto;
using ET.SOWRoles.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ET.SoW.ExportDtos
{
    [AutoMapFrom(typeof(SoWRoleDetailItemDto))]
    public class SoWExportItem : IExcelRowExport
    {
        [Display(Name="Bill", Order = 1)]
        public string IsBillable { get; set; }

        [Display(Name="Billing Type", Order = 2)]
        public string BillingType { get; set; }

        [Display(Name="Role Name", Order = 3)]
        public string RoleName { get; set; }

        [Display(Name="Rate Type", Order = 4)]
        public string RateType { get; set; }

        [Display(Name="Currency", Order = 5)]
        public string Currency { get; set; }

        [Display(Name= "Standard Rate", Order = 6)]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public double? StandardRate { get; set; }

        [Display(Name="Actual Rate", Order = 7)]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public double? ActualRate { get; set; }

        [Display(Name="FTE", Order = 8)]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public double? FTE { get; set; }

        [Display(Name= "Total Hours", Order = 9)]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public int? TotalHours { get; set; }

        [Display(Name= "Total Hours/Month", Order = 10)]
        public int? TotalHoursPerMonth { get; set; }

        [Display(Name= "Start Date", Order = 11)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartDate { get; set; }

        [Display(Name= "End Date", Order = 12)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "RC FEE", Order = 13, GroupName = "FIX FEE")]
        public IList<IKeyValue> FixRateCardFeeKV { get; set; }

        [Display(Name = "FC Fee", Order = 14, GroupName = "FIX FEE")]
        public IList<IKeyValue> FixForecastFeeKV { get; set; }
        
        [Display(Name = "RATE CARD FEE", Order = 15, GroupName = "MONTHLY FEE")]
        public IList<IKeyValue> MonthlyRateCardFeeKV { get; set; }

        [Display(Name = "FORECAST FEE", Order = 16, GroupName = "MONTHLY FEE")]
        public IList<IKeyValue> MonthlyForecastFeeKV { get; set; }

        [Display(Name = "Term (days)", Order = 17)]
        public int? Term { get; set; }

        [Display(Name = "Billing Note", Order = 18)]
        public string Description { get; set; }

        public int StartCellIndex { get; set; }
    }
}
