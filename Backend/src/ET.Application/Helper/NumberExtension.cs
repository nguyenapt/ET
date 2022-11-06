using System;

namespace ET.Helper
{
    public static class NumberExtension
    {
        public static double? RoundNumber(this double? number)
        {
            if (!number.HasValue)
                return null;
            return Math.Round(number.Value, 2, MidpointRounding.AwayFromZero);
        }
    }
}