using System.Data;

namespace ET.TimesheetEntries.Dto
{
    public class GetApprovedOrRejectedTimeSheetInformationRequestDto
    {
        public DataTable TimeSheetEntryIds { get; set; }
    }
}
