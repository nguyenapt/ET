using ET.Shared.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ET.SoW.ExportDtos
{
    public class SoWExportFooter : IExcelRowExport
    {
        [Display(Name = "Totals", Order = 1)]
        public string TotalText { get; set; }

        [Display(Name = "Fix RateCard Fee", Order = 2)]
        public IList<IKeyValue> FixRateCardFeeKV { get; set; }

        [Display(Name = "Fix Forecast Fee", Order = 3)]
        public IList<IKeyValue> FixForecastFeeKV { get; set; }

        [Display(Name = "Monthly RateCard Fee", Order = 4)]
        public IList<IKeyValue> MonthlyRateCardFeeKV { get; set; }

        [Display(Name = "Monthly Fix ForecastFee", Order = 5)]
        public IList<IKeyValue> MonthlyFixForecastFeeKV { get; set; }

        public int StartCellIndex { get; set; }
    }
}
