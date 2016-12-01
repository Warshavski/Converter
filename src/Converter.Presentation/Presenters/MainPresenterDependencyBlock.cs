using Escyug.Converter.Common.Logging;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Services;
using Escyug.Converter.Models.Utils;
using Escyug.Converter.Models.Utils.MailSender;

namespace Escyug.Converter.Presentation.Presenters
{
    public class MainPresenterDependencyBlock : IMainPresenterDependencyBlock
    {
        public ILogManager LogManager { get; private set; }
        public IMailSender MailSender { get; private set; }

        public IConfigurationManager<SenderConfiguration> SenderConfigManager { get; private set; }

        public EntityService<RecipeRow> RecipeService { get; private set; }
        public EntityService<RemainRow> RemainService { get; private set; }

        public IRepository<RecipeRow> RecipeRepository { get; private set; }
        public IRepository<RemainRow> RemainRepository { get; private set; }

        public IGuideService GuideService { get; private set; }

        public IConfigurationManager<FtpConfiguration> FtpConfigManager  { get; private set; }

        public MainPresenterDependencyBlock(
            ILogManager logManager,
            IMailSender mailSender,
            IConfigurationManager<SenderConfiguration> senderConfigManager,
            EntityService<RecipeRow> recipeService,
            EntityService<RemainRow> remainService,
            IRepository<RecipeRow> recipeRepository,
            IRepository<RemainRow> remainRepository,
            IGuideService guideService,
            IConfigurationManager<FtpConfiguration> ftpConfigManager)
        {
            LogManager = logManager;
            MailSender = mailSender;

            SenderConfigManager = senderConfigManager;

            RecipeService = recipeService;
            RemainService = remainService;

            RecipeRepository = recipeRepository;
            RemainRepository = remainRepository;

            GuideService = guideService;

            FtpConfigManager = ftpConfigManager;
        }
    }
}
