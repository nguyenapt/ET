using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ET.TimesheetEntries.Dto
{
    public class TimesheetDateRequestDto : ICustomValidate
    {
        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (StartDate > EndDate)
            {
                context.Results.Add(new ValidationResult("Start date cannot be greater than End date"));
            }
        }
    }
}
