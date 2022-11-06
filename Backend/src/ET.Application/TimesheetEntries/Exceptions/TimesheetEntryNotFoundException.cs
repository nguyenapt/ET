using System;

namespace ET.TimesheetEntries.Exceptions
{
    public class TimesheetEntryNotFoundException : Exception
    {
        public TimesheetEntryNotFoundException(string message) : base(message)
        {
        }
    }
}
