namespace ET.SiteSetting.Dto
{
    public class ETSiteSetting
    {
        public ETSiteSetting(string name, string displayname, string value)
        {
            Name = name;
            DisplayName = displayname;
            Value = value;
        }
        public string Name { get; set; }
        public string Value { get; set; }
        public string DisplayName { get; set; }
    }
}
