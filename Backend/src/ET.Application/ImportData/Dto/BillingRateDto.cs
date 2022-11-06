using System;
using System.Globalization;
using Abp.AutoMapper;
using Npoi.Mapper.Attributes;

namespace ET.ImportData.Dto
{
    [AutoMapTo(typeof(Entities.BillingRate))]
    public class BillingRateDto
    {
        [Column("ResourceRole")]
        public string ResourceRole { get; set; }
        
        [Column("Currency")]
        public string Currency { get; set; }

        [Column("EffectiveTime")]
        public string EffectiveTime { get; set; }

        [Column("MonthlyRate")]
        public string MonthlyRate { get; set; }

        [Column("DailyRate")]
        public string DailyRate { get; set; }

        [Column("HourlyRate")]
        public string HourlyRate { get; set; }

        public DateTime EffectiveDate
        {
            get
            {
                var dateFormats = new[] { "d/M/yyyy", "d/M/yyyy hh:mm:ss tt", "d/M/yyyy HH:mm:ss" };
                if (!string.IsNullOrEmpty(EffectiveTime) && DateTime.TryParseExact(EffectiveTime, dateFormats, null, DateTimeStyles.None, out var dt))
                {
                    return dt;
                }
                return default;
            }
        }

    }
}
