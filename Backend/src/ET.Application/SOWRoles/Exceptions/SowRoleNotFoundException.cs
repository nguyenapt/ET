using System;

namespace ET.SOWRoles.Exceptions
{
    public class SowRoleNotFoundException : Exception
    {
        public SowRoleNotFoundException(string message) : base(message)
        {
        }
    }
}
