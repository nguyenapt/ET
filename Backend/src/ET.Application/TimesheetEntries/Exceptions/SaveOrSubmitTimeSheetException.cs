using System;

namespace ET.TimesheetEntries.Exceptions
{
    public class SaveOrSubmitTimeSheetException : Exception
    {
        public SaveOrSubmitTimeSheetException(string message) : base(message) { }
    }
}
