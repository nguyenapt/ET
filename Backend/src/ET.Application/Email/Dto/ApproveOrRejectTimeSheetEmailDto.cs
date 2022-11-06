namespace ET.Email.Dto
{
    public class ApproveOrRejectTimeSheetEmailDto
    {
        public string SubmitterFullName { get; set; }
        public string SubmitterEmail { get; set; }
        public string ApprovalFullName { get; set; }
        public string TimeSheetLink { get; set; }
        public string Comment { get; set; }
        public string SkypeSupport { get; set; }
        public string EmailSupport { get; set; }
    }
}
