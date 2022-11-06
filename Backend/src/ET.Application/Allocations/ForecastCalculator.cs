using System;
using System.Collections.Generic;
using Abp.Dependency;

namespace ET.Allocations
{
    public interface IForecastCalculator : ITransientDependency
    {
        public double Calculate(DateTime startDate, DateTime endDate, double estimateHoursWeek, double actualRate);
        public double GetBusinessDaysNumber(DateTime startDate, DateTime endDate, IEnumerable<DateTime> bankHolidays);
    }
    public class ForecastCalculator : IForecastCalculator
    {
        public double Calculate(DateTime startDate, DateTime endDate, double estimateHoursWeek, double actualRate)
        {
            var businessDayNumbers = GetBusinessDaysNumber(startDate, endDate, null);
            return Math.Round(businessDayNumbers / 5 * estimateHoursWeek * actualRate, 2, MidpointRounding.AwayFromZero);
        }

        public double GetBusinessDaysNumber(DateTime startDate, DateTime endDate, IEnumerable<DateTime> bankHolidays)
        {
            startDate = startDate.Date;
            endDate = endDate.Date;
            if (startDate > endDate)
                throw new ArgumentException("Incorrect last day " + endDate);

            var span = endDate - startDate;
            var businessDays = span.Days + 1;
            var fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceeding the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                var firstDayOfWeek = startDate.DayOfWeek == DayOfWeek.Sunday
                    ? 7 : (int)startDate.DayOfWeek;
                var lastDayOfWeek = endDate.DayOfWeek == DayOfWeek.Sunday
                    ? 7 : (int)endDate.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            // subtract the number of bank holidays during the time interval
            if (bankHolidays == null) return businessDays;

            foreach (var bankHoliday in bankHolidays)
            {
                var bh = bankHoliday.Date;
                if (startDate <= bh && bh <= endDate)
                    --businessDays;
            }

            return businessDays;
        }
    }
}
