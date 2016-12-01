using System.Collections.Generic;

namespace Escyug.Converter.Models.Entities.Guides
{
    public class GuidesIdsCollection
    {
        public HashSet<int> MnnGuideIs { get; set; }
        public HashSet<int> TradeNamesGudeIds { get; set; }
        public HashSet<int> DrugGuideIds { get; set; }
        public HashSet<int> DrugformIds { get; set; }
    }
}
