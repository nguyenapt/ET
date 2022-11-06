namespace ET.SOWRoles.SowRoleValidators
{
    public class SowRoleValidateMessage
    {
        public const string RateTypeShouldBe = "Rate Type should be {0}";
        public const string IsRequired = "{0} is required";
        public const string MRTRequiredMsg = "All MRT, requires FTE >= 1 or more.";
        public const string TMFTRequiredMsg = "All TMFT, requires FTE >= 1 or more.";
        public const string TMPTRequiredMsg = "FTE must be >= 0.";
        public const string TotalValueRequired = "Can only input Total Hours value";
        public const string MonthlyValueRequired = "Can only input Total Hours/Monthly value";
        public const string NoMonthlyAndTotal = "There is no Total Hours or Total Hours/Monthly value.";
    }

    public class SowValidateMessage
    {
        public const string StatusNoteRequired = "Status Note Is Required";
        public const string InvoicingCycle = "Invoicing Cycle should be Monthly or Quarterly";
    }
}
