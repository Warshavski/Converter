using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Entities.Guides;
using Escyug.Converter.Models.Repositories;

namespace Escyug.Converter.Models.Services
{
    public class GuideServiceDependencyBlock : IGuideServiceDependencyBlock
    {
        public IConfigurationManager<FtpConfiguration> FtpConfigurationManager { get; }
        public IConfigurationManager<GuidesConfiguration> GuidesConfigurationManager { get; }

        public IGuideRepository<Mnn> MnnGuideRepository { get; }
        public IGuideRepository<TradeName> TradeNameGuideRepository { get; }
        public IGuideRepository<Drug> DrugGuideRepository { get; }
        public IGuideRepository<Drugform> DrugformGuideRepository { get; }

        public GuideServiceDependencyBlock(
            IConfigurationManager<FtpConfiguration> ftpConfigurationManager,
            IConfigurationManager<GuidesConfiguration> guidesConfigurationManager,
            IGuideRepository<Mnn> mnnGuideRepository,
            IGuideRepository<TradeName> tradeNameGuideRepository,
            IGuideRepository<Drug> drugGuideRepository,
            IGuideRepository<Drugform> drugformGuideRepository
            )
        {
            FtpConfigurationManager = ftpConfigurationManager;
            GuidesConfigurationManager = guidesConfigurationManager;
            MnnGuideRepository = mnnGuideRepository;
            TradeNameGuideRepository = tradeNameGuideRepository;
            DrugGuideRepository = drugGuideRepository;
            DrugformGuideRepository = drugformGuideRepository;
        }
    }
}
