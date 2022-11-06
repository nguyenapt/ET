using System;

namespace ET.TaskCategories.Exceptions
{
    public class TaskCategoryNotFoundException : Exception
    {
        public TaskCategoryNotFoundException(string message) : base(message)
        {
        }
    }
}