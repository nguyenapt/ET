namespace ET.TimesheetEntries.Dto
{
    public class GetTimeSheetSubmitterInformationRequest
    {
        public long UserId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}