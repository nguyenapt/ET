using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ET.TimesheetEntries.Dto
{
    public class TimePeriodDto : ICustomValidate
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            var validationResult = new List<ValidationResult>();
            if (StartDate > EndDate)
            {
                validationResult.Add(new ValidationResult("End date should be greater than or equal to Start date"));
            }

            if (validationResult.Any())
            {
                context.Results.AddRange(validationResult);
            }
        }
    }
}
