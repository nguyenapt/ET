using System;

namespace ET.TimesheetEntries.Exceptions
{
    public class ApprovalInformationNotFoundException : Exception
    {
        public ApprovalInformationNotFoundException(string message) : base(message)
        {
        }
    }
}
