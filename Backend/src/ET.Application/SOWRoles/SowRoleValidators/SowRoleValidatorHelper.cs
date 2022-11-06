using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ET.SOWRoles.Dto;
using System;

namespace ET.SOWRoles.SowRoleValidators
{
    public class SowRoleValidatorHelper
    {
        public static List<ValidationResult> Validate(ISowRoleDto sowRole)
        {
            var validationResult = new List<ValidationResult>();

            var billingTypeSplit = sowRole.BillingType.Split("-");

            if (billingTypeSplit.Length == 2)
            {
                if (billingTypeSplit[0] == "MRT")
                {
                    if (sowRole.RateType != "Monthly")
                        validationResult.Add(new ValidationResult(FormatMsg(1, "Monthly"), new[] { "RateType" }));
                    if (!sowRole.FTE.HasValue || sowRole.FTE.Value < 1)
                        validationResult.Add(new ValidationResult(SowRoleValidateMessage.MRTRequiredMsg, new[] { "FTE" }));
                }

                if (billingTypeSplit[0] == "TMFT")
                {
                    if (sowRole.RateType != "Daily")
                        validationResult.Add(new ValidationResult(FormatMsg(1, "Daily"), new[] { "RateType" }));
                    if (!sowRole.FTE.HasValue || sowRole.FTE.Value < 1)
                        validationResult.Add(new ValidationResult(SowRoleValidateMessage.TMFTRequiredMsg, new[] { "FTE" }));
                }

                if (billingTypeSplit[0] == "TMPT")
                {
                    if (sowRole.RateType != "Hourly")
                        validationResult.Add(new ValidationResult(FormatMsg(1, "Hourly"), new[] { "RateType" }));
                    if (sowRole.FTE.HasValue && sowRole.FTE.Value < 0)
                        validationResult.Add(new ValidationResult(SowRoleValidateMessage.TMPTRequiredMsg, new[] { "FTE" }));
                }
                
                if (!sowRole.StartDate.HasValue)
                    validationResult.Add(new ValidationResult(FormatMsg(2, "Start Date"), new[] { "StartDate" }));

                if (billingTypeSplit[1] == "T" || billingTypeSplit[1] == "TB")
                {
                    if (!sowRole.EndDate.HasValue)
                        validationResult.Add(new ValidationResult(FormatMsg(2, "End Date"), new[] { "EndDate" }));
                    if ((!sowRole.TotalHours.HasValue || sowRole.TotalHoursPerMonth.HasValue) && billingTypeSplit[1] == "TB")
                        validationResult.Add(new ValidationResult(SowRoleValidateMessage.TotalValueRequired, new[] { "TotalHours" }));
                    if ((sowRole.TotalHours.HasValue || sowRole.TotalHoursPerMonth.HasValue) && billingTypeSplit[1] == "T")
                        validationResult.Add(new ValidationResult(SowRoleValidateMessage.NoMonthlyAndTotal, new[] { "TotalHours" }));
                }

                if (billingTypeSplit[1] == "C" || billingTypeSplit[1] == "CB")
                {
                    if (!sowRole.Term.HasValue && (!billingTypeSplit[0].Equals("MRT", StringComparison.InvariantCultureIgnoreCase)
                        || !billingTypeSplit[1].Equals("C", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        validationResult.Add(new ValidationResult(FormatMsg(2, "Term"), new[] { "Term" }));
                    }                 
                    if ((sowRole.TotalHours.HasValue || !sowRole.TotalHoursPerMonth.HasValue) && billingTypeSplit[1] == "CB")
                        validationResult.Add(new ValidationResult(SowRoleValidateMessage.MonthlyValueRequired, new[] { "TotalHoursPerMonth" }));
                    if ((sowRole.TotalHours.HasValue || sowRole.TotalHoursPerMonth.HasValue) && billingTypeSplit[1] == "C")
                        validationResult.Add(new ValidationResult(SowRoleValidateMessage.NoMonthlyAndTotal, new[] { "TotalHours" }));
                }
            }

            return validationResult;
        }

        private static string FormatMsg(int msgCase, string value)
        {
            return msgCase switch
            {
                1 => string.Format(SowRoleValidateMessage.RateTypeShouldBe, value),
                2 => string.Format(SowRoleValidateMessage.IsRequired, value),
                _ => string.Empty
            };
        }
    }
}
