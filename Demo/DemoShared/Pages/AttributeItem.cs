// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

namespace DemoShared
{
    public class AttributeItem
    {
        public AttributeItem() { }

        public AttributeItem(string Name, string Description, string DefaultValue, string Type = "string", string ValueList = "-")
        {
            this.Name = Name;
            this.Description = Description;
            this.Type = Type;
            this.ValueList = ValueList;
            this.DefaultValue = DefaultValue;
        }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; } = "string";
        public string? ValueList { get; set; } = "-";
        public string? DefaultValue { get; set; } = "";

    }
}
