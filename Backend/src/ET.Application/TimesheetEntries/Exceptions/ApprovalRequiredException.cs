using System;

namespace ET.TimesheetEntries
{
    public class ApprovalRequiredException : Exception
    {
        public ApprovalRequiredException(string message) : base(message)
        {
        }
    }
}