using System;
using System.Collections.Generic;
using System.Text;

namespace ET.TimesheetEntries.Exceptions
{
    public class ApproveOrRejectTimeSheetException : Exception
    {
        public ApproveOrRejectTimeSheetException(string message) : base(message)
        {
        }
    }
}
