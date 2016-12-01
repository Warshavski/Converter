using System.Collections.Generic;

namespace Escyug.Converter.Models.Entities.Configurations
{
    public class GuidesConfiguration
    {
        public string Path { get; set; }
        public string LastUpdateDate { get; set; }
        public Dictionary<string, string[]> Guides { get; set; }
    }
}
