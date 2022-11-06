using System;

namespace ET.TimesheetEntries.Exceptions
{
    public class HasMoreThanOneSubmitterException : Exception
    {
        public HasMoreThanOneSubmitterException(string message) : base(message)
        {
        }
    }
}