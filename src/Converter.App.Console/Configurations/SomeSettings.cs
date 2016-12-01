using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.Converter.App.Console.Configurations
{
    public class SomeSettings : ConfigurationSection
    {
        private SomeSettings() { }

        [ConfigurationProperty("TextSize", DefaultValue = "8.5")]
        public float TextSize
        {
            get { return (float)this["TextSize"]; }
            set { this["TextSize"] = value; }
        }

        [ConfigurationProperty("FillOpacity", DefaultValue = "40")]
        public byte FillOpacity
        {
            get { return (byte)this["FillOpacity"]; }
            set { this["FillOpacity"] = value; }
        }
    }
}
