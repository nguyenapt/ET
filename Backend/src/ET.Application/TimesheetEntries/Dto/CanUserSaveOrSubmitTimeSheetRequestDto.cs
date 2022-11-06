using System.Data;

namespace ET.TimesheetEntries.Dto
{
    public class CanUserSaveOrSubmitTimeSheetRequestDto
    {
        public DataTable TimeSheetEntryIds { get; set; }
        public long UserId { get; set; }
    }
}