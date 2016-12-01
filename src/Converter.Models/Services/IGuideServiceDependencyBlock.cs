using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Entities.Guides;
using Escyug.Converter.Models.Repositories;

namespace Escyug.Converter.Models.Services
{
    public interface IGuideServiceDependencyBlock
    {
        IConfigurationManager<FtpConfiguration> FtpConfigurationManager { get; }
        IConfigurationManager<GuidesConfiguration> GuidesConfigurationManager { get; }

        IGuideRepository<Mnn> MnnGuideRepository { get; }
        IGuideRepository<TradeName> TradeNameGuideRepository { get; }
        IGuideRepository<Drug> DrugGuideRepository { get; }
        IGuideRepository<Drugform> DrugformGuideRepository { get; }
    }
}
