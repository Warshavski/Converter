using System;
using Escyug.Converter.Common.Logging;
using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Utils.MailSender;

namespace Escyug.Converter.Presentation.Presenters.junk
{
    public class SettingsPresenterDependencyBlock : ISettingsPresenterDependencyBlock
    {
        public ILogManager LogManager { get; }
        public IMailSender MailSender { get; }

        public IConfigurationManager<SenderConfiguration> SenderConfigurationManager { get;  }
        public IConfigurationManager<ConnectionStringConfiguration> ConnectionConfigurationManager { get; }
        public IConfigurationManager<FtpConfiguration> FtpConfigurationManager { get; }
        public IConfigurationManager<GuidesConfiguration> GuidesConfigurationManager { get; }
        public IConfigurationManager<ConverterTask> TaskConfigurationManager { get; }
        public IConfigurationManager<WebServiceConfiguration> WebServiceConfigurationManager { get; }

        public IRepository<LogRow> LogsRepository { get; }


        public SettingsPresenterDependencyBlock(
            ILogManager logManager,
            IMailSender mailSender,
            IConfigurationManager<SenderConfiguration> senderConfigurationManager,
            IConfigurationManager<ConnectionStringConfiguration> conectionConfigurationManager,
            IConfigurationManager<FtpConfiguration> ftpConfigurationManager,
            IConfigurationManager<GuidesConfiguration> guidesConfigurationManager,
            IConfigurationManager<ConverterTask> taskConfigurationManager,
            IConfigurationManager<WebServiceConfiguration> webServiceConfigurationManager,
            IRepository<LogRow> logsRepository)
        {
            LogManager = logManager;
            MailSender = mailSender;

            SenderConfigurationManager = senderConfigurationManager;
            ConnectionConfigurationManager = conectionConfigurationManager;
            FtpConfigurationManager = ftpConfigurationManager;
            GuidesConfigurationManager = guidesConfigurationManager;
            TaskConfigurationManager = taskConfigurationManager;
            WebServiceConfigurationManager = webServiceConfigurationManager;

            LogsRepository = logsRepository;
        }
    }
}
