using System.Data;

namespace ET.TimesheetEntries.Dto
{
    public class CanUserApproveOrRejectThisTimeSheetRequestDto
    {
        public DataTable TimeSheetEntryIds { get; set; }
        public long UserId { get; set; }
    }
}
