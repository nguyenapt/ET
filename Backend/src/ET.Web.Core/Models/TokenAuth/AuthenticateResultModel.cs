using ET.Authorization.Users;
using System;
using System.Collections.Generic;

namespace ET.Models.TokenAuth
{
    public class AuthenticateResultModel
    {
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public User User { get; set; }

        public Guid? ResourceId { get; set; }
        public IList<string> Roles { get; set; }
    }
}
