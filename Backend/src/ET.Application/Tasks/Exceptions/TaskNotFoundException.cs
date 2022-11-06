using System;

namespace ET.Tasks.Exceptions
{
    public class TaskNotFoundException : Exception
    {
        public TaskNotFoundException(string message) : base(message)
        {
        }
    }
}