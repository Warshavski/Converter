using System.Configuration;

namespace Escyug.Converter.Configuration.Sections
{
    internal class GuidesSectionConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("path")]
        public GuidesPathElement Path
        {
            get { return (GuidesPathElement)this["path"]; }
            set { this["path"] = value; }
        }

        [ConfigurationProperty("lastUpdateDate")]
        public GuidesLastUpdateDateElement LastUpdateDate
        {
            get { return (GuidesLastUpdateDateElement)this["lastUpdateDate"]; }
            set { this["lastUpdateDate"] = value; }
        }

        [ConfigurationProperty("guides")]
        [ConfigurationCollection(typeof(GuidesElement), AddItemName = "guide")]
        public GuidesElement Guides
        {
            get { return (GuidesElement)this["guides"]; }
            set { this["guides"] = value; }
        }

    }

    
    internal class GuidesPathElement : ConfigurationElement
    {
        [ConfigurationProperty("value", DefaultValue = "", IsRequired = true)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }

    internal class GuidesLastUpdateDateElement : ConfigurationElement
    {
        [ConfigurationProperty("value", DefaultValue = "", IsRequired = true)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }

    internal class GuideElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("file", DefaultValue = "", IsRequired = true)]
        public string File
        {
            get { return (string)this["file"]; }
            set { this["file"] = value; }
        }

        [ConfigurationProperty("date", DefaultValue = "", IsRequired = true)]
        public string Date
        {
            get { return (string)this["date"]; }
            set { this["date"] = value; }
        }
    }

    internal class GuidesElement : ConfigurationElementCollection
    {
        public GuideElement this[int index]
        {
            get { return base.BaseGet(index) as GuideElement; }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new GuideElement this[string responseString]
        {
            get { return (GuideElement)BaseGet(responseString); }
            set
            {
                if (BaseGet(responseString) != null)
                {
                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
                }
                BaseAdd(value);
            }
        }

        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new GuideElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GuideElement)element);
        }
    }
}
