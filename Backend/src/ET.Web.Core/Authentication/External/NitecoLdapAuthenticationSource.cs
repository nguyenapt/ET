using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using ET.Authorization.Users;
using ET.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Authentication.External
{
    public class NitecoLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public NitecoLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig) : base(settings, ldapModuleConfig)
        {
        }
    }
}
