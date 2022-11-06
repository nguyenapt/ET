using System.ComponentModel;

namespace ET
{
    public class AppEnums
    {
        public enum SowStatus
        {
            Draft, 
            Open, 
            Confirmed, 
            Signed, 
            Closed, 
            Rejected
        }

        public enum BillingType
        {
            [Description("MRT-C")]
            MRTC,
            [Description("TMFT-T")]
            TMFTT,
            [Description("TMFT-C")]
            TMFTC,
            [Description("TMFT-TB")]
            TMFTTB,
            [Description("TMFT-CB")]
            TMFTCB,
            [Description("TMPT-T")]
            TMPTT,
            [Description("TMPT-C")]
            TMPTC,
            [Description("TMPT-TB")]
            TMPTTB,
            [Description("TMPT-CB")]
            TMPTCB
        }

        public enum RateType
        {
            Monthly,
            Daily,
            Hourly
        }

        public enum EInvoicingCycle
        {
            Monthly,
            Quarterly
        }
    }
}
