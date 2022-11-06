using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Configuration;
using ET.Authorization;
using ET.Shared.Dto;
using ET.SiteSetting.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ET.SiteSetting
{
    [AbpAuthorize(PermissionNames.Pages_SiteSettings)]
    public class SiteSettingAppService : ApplicationService, ISiteSettingAppService
    {
        private readonly ISettingManager _settingManager;

        public SiteSettingAppService(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }
        
        public Dictionary<string, List<ETSiteSetting>> GetSiteSettingDefinitions()
        {
            return _settingManager.GetAllSettingValues().GroupBy(RuleForGroupBy)
                .ToDictionary(x => x.Key, x => x.Select(s => new ETSiteSetting(s.Name, GetDisplayName(s.Name), s.Value)).ToList());
        }

        [HttpPost]
        public bool ChangeSiteSettingDefinitions(IEnumerable<KeyValueDto> changeValues)
        {
            foreach (var changeValue in changeValues)
            {
                _settingManager.ChangeSettingForApplication(changeValue.Name, changeValue.Value);
            }
            return true;
        }

        private string RuleForGroupBy(ISettingValue setting)
        {
            if (setting.Name.Contains("Mail")) return ETConsts.ETSettingDefinitions.EmailSettingGroupName;
            if (setting.Name.Contains("Ldap")) return ETConsts.ETSettingDefinitions.LdapGroupName;
            if (setting.Name.Contains("UserManagement")) return ETConsts.ETSettingDefinitions.UserManagementGroupName;
            if (setting.Name.Contains("EmailTemplate")) return ETConsts.ETSettingDefinitions.EmailTemplateGroupName;

            return ETConsts.ETSettingDefinitions.SiteSettingGroupName;
        }

        private static string GetDisplayName(string name)
        {
            if (!name.Contains(".")) return name;
            var nameStrings = name.Split(".");
            var length = nameStrings.Length;
            var displayName = length >= 2 ? string.Concat(nameStrings[length - 2]," -", nameStrings[length - 1]) : nameStrings.LastOrDefault();
            return string.Concat(displayName.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }
    }
}
