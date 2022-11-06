using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ET.TimesheetEntries.Dto
{
    public class RejectTimeSheetRequestDto
    {
        [Required]
        public List<Guid> TimeSheetEntryIds { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
