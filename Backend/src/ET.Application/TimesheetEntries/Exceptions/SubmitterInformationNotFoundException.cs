using System;

namespace ET.TimesheetEntries.Exceptions
{
    public class SubmitterInformationNotFoundException : Exception
    {
        public SubmitterInformationNotFoundException(string message) : base(message)
        {
        }
    }
}