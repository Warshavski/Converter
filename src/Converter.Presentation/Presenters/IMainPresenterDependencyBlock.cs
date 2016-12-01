using Escyug.Converter.Common.Logging;
using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Services;
using Escyug.Converter.Models.Utils.MailSender;

namespace Escyug.Converter.Presentation.Presenters
{
    public interface IMainPresenterDependencyBlock
    {
        ILogManager LogManager { get; }
        IMailSender MailSender { get; }

        IConfigurationManager<SenderConfiguration> SenderConfigManager { get; }

        EntityService<RecipeRow> RecipeService { get; }
        EntityService<RemainRow> RemainService { get; }

        IRepository<RecipeRow> RecipeRepository { get; }
        IRepository<RemainRow> RemainRepository { get; }

        IGuideService GuideService { get; }

        IConfigurationManager<FtpConfiguration> FtpConfigManager { get; }
    }
}
