using System.Collections.Generic;
using ET.Shared.Dto;
using ET.SiteSetting.Dto;

namespace ET.SiteSetting
{
    public interface ISiteSettingAppService
    {
        public Dictionary<string, List<ETSiteSetting>> GetSiteSettingDefinitions();
        public bool ChangeSiteSettingDefinitions(IEnumerable<KeyValueDto> changeValues);
    }
}
