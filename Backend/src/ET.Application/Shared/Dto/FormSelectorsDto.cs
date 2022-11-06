using System.Collections.Generic;

namespace ET.Shared.Dto
{
    public class FormSelectorsDto
    {
        public FormSelectorsDto(string key, IEnumerable<KeyValueDto> selectors)
        {
            Key = key;
            Selectors = selectors;
        }
        public string Key { get; set; }
        public IEnumerable<KeyValueDto> Selectors { get; set; }
    }
}
