using System.Collections.Generic;

namespace Escyug.Converter.Models.Entities.Guides
{
    public class GuidesCollection
    {
        public HashSet<Mnn>  MnnGuide { get; }
        public HashSet<TradeName> TradeNameGuide { get; }

        public GuidesCollection(HashSet<Mnn> mnnGuide, HashSet<TradeName> tradeNameGuide)
        {
            MnnGuide = mnnGuide;
            TradeNameGuide = tradeNameGuide;
        }
    }
}
