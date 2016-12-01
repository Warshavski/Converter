using System;
using System.Threading.Tasks;

using Escyug.Converter.Models.Entities.Guides;

namespace Escyug.Converter.Models.Services.Junk
{
    public interface IGuideService
    {
        event EventHandler<GuideServiceEventArgs> StateChanged;
        GuidesCollection GetGuides();
        GuidesIdsCollection GetGuidesIds();
        Task<GuidesIdsCollection> GetGuidesIdsAsync();
    }
}
