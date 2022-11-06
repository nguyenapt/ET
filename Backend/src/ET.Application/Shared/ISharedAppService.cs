using System.Collections.Generic;
using ET.Shared.Dto;

namespace ET.Shared
{
    public interface ISharedAppService
    {
        public Dictionary<string, IEnumerable<KeyValueDto>> GetFormSelectors(List<string> selectors);
    }
}
