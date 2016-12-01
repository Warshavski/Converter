using Escyug.Converter.Common.Logging;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Utils.MailSender;

namespace Escyug.Converter.Presentation.Presenters.junk
{
    public interface ISettingsPresenterDependencyBlock
    {
        ILogManager LogManager { get;  }
        IMailSender MailSender { get;  }

        IConfigurationManager<SenderConfiguration> SenderConfigurationManager { get;  }
        IConfigurationManager<ConnectionStringConfiguration> ConnectionConfigurationManager { get; }
        IConfigurationManager<FtpConfiguration> FtpConfigurationManager { get; }
        IConfigurationManager<GuidesConfiguration> GuidesConfigurationManager { get; }
        IConfigurationManager<ConverterTask> TaskConfigurationManager { get; }
        IConfigurationManager<WebServiceConfiguration> WebServiceConfigurationManager { get; }
        IRepository<LogRow> LogsRepository { get; }
    }
}
