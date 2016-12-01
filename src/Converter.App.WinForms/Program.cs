using System;
using System.Configuration;
using System.Windows.Forms;

using Microsoft.Win32;

using log4net.Config;

using Escyug.Converter.App.WinForms.Forms;
using Escyug.Converter.App.WinForms.UserControls;
using Escyug.Converter.Common.Logging;

using Escyug.Converter.Configuration;
using Escyug.Converter.Data.Ado;
using Escyug.Converter.Data.Ado.Repositories;
using Escyug.Converter.Data.Txt.Repositories;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Entities.Guides;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Services;
using Escyug.Converter.Models.Utils.MailSender;
using Escyug.Converter.Models.Utils.XmlConverter;

using Escyug.Converter.Presentation.Common;
using Escyug.Converter.Presentation.Presenters;
using Escyug.Converter.Presentation.Views;
using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.RecipesServiceReference;
using Escyug.Converter.Models.RemainsServiceReference;

namespace Escyug.Converter.App.WinForms
{
    internal static class Program
    {
        internal static readonly ApplicationContext Context = new ApplicationContext();

        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        internal static void Main(string[] args)
        {

#if DEBUG
            args = new string[1];
            args[0] = "-noui";
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //*** (NOT USED) RegisterInStartup(true);

            var controller = new ApplicationController(new NinjectAdapter())
                    .RegisterInstance(new ApplicationContext())
                    .RegisterService<IMainPresenterDependencyBlock, MainPresenterDependencyBlock>()
                    .RegisterService<IGuideServiceDependencyBlock, GuideServiceDependencyBlock>();

            ConfigureDataAccess(controller);
            ConfigureServices(controller);
            ConfigureUtils(controller);
            ConfigureLogging(controller);
            ConfigureViews(controller);


            if (args.Length > 0 && string.Equals(args[0], "-noui"))
            {
                controller.Run<MainPresenter>();
            }
            else
            {
                controller.Run<SettingsCompositePresenter>();
            }

        }

        //*** (NOT USED) startup application on windows start 
        private static void RegisterInStartup(bool isChecked)
        {
            var applicationName = "Converter.App.WinForms";
            var registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (isChecked)
            {
                registryKey.SetValue(applicationName, Application.ExecutablePath);
            }
            else
            {
                registryKey.DeleteValue(applicationName);
            }
        }



        // UTILS CONFIGURE SECTION
        //---------------------------------------------------------------------

        /**
        private static void ConfigureSender(IApplicationController controller)
        {
           
            var senderConfig = config.Sections["senderConfiguration"] as SenderSectionConfiguration;
           
            var host = senderConfig.Host.Address;
            var port = senderConfig.Host.Port;
            var from = new string[] 
                { 
                    senderConfig.Credentials.Login,
                    senderConfig.Credentials.Password
                };

            var to = new string[senderConfig.Receivers.Count];
            for (int i = 0; i < senderConfig.Receivers.Count; ++i)
            {
                to[i] = senderConfig.Receivers[i].Address;
            }

            #if DEBUG

            System.Diagnostics.Debug.WriteLine(host);
            System.Diagnostics.Debug.WriteLine(port);
            System.Diagnostics.Debug.WriteLine(from[0] + " " + from[1]);
            System.Diagnostics.Debug.WriteLine(to[0] + " " + to[1]);

            #endif

            var mailSender = new MailSender(host, port, from, to);
            controller.RegisterInstance<IMailSender>(mailSender);
            
        }
        */

        private static void ConfigureUtils(IApplicationController controller)
        {
            controller.RegisterService<IXmlConverter<RemainRow>, RemainXmlConverter>();
            controller.RegisterService<IXmlConverter<RecipeRow>, RecipeXmlConverter>();

            controller.RegisterService<IMailSender, MailSender>();
        }

        private static void ConfigureLogging(IApplicationController controller)
        {
            XmlConfigurator.Configure();

            var logManager = new LogManagerAdapter();

            controller.RegisterInstance<ILogManager>(logManager);

            //*** check for correct implementation
            // logs all unhandled exceptions
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var log = logManager.GetLog(typeof(Program));
                var exception = (Exception)e.ExceptionObject;
                log.Error(exception.ToString());
            };
        }



        // DATA ACCESS CONFIGURE SECTION
        //---------------------------------------------------------------------

        //*** create factory (different DbContext for RecipeRow and RemainRow)
        //*** how to use config manager
        private static void ConfigureDataAccess(IApplicationController controller)
        {
            // APP CONFIG SECTION
            //---------------------------------------------

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var senderConfig = new SenderConfigurationManager(config);
            controller.RegisterInstance<IConfigurationManager<SenderConfiguration>>(senderConfig);

            var connectionConfig = new ConnectionStringsConfigurationManager(config);
            controller.RegisterInstance<IConfigurationManager<ConnectionStringConfiguration>>(connectionConfig);

            var ftpConfig = new FtpConfigurationManager(config);
            controller.RegisterInstance<IConfigurationManager<FtpConfiguration>>(ftpConfig);

            var guidesConfig = new GuidesConfigurationManager(config);
            controller.RegisterInstance<IConfigurationManager<GuidesConfiguration>>(guidesConfig);

            var webServiceConfig = new WebServiceConfigurationManager(config);
            controller.RegisterInstance<IConfigurationManager<WebServiceConfiguration>>(webServiceConfig);


            controller.RegisterService<IConfigurationManager<ConverterTask>, ConverterTaskConfigurationManager>();



            // DB CONTEXT SECTION
            //---------------------------------------------

            var recipesConnection = connectionConfig.Get("recipes");
            var recipesDbContext = new DbContext(recipesConnection.ConnectionString, recipesConnection.ProviderName);

            var remainsConnection = connectionConfig.Get("remains");
            var remainsDbContext = new DbContext(remainsConnection.ConnectionString, remainsConnection.ProviderName);

            var guidesConnection = connectionConfig.Get("guides");
            var guidesDbContext = new DbContext(guidesConnection.ConnectionString, guidesConnection.ProviderName);


            //*** inject into IRepositoty<RecipeRow>
            controller.RegisterInstance<DbContext, IRepository<RecipeRow>>(recipesDbContext);

            //*** inject into IRepositoty<RemainRow>
            controller.RegisterInstance<DbContext, IRepository<RemainRow>>(remainsDbContext);

            //*** inject into guides repositories
            controller.RegisterInstance<DbContext, IRepository<Mnn>>(guidesDbContext);
            controller.RegisterInstance<DbContext, IRepository<TradeName>>(guidesDbContext);
            controller.RegisterInstance<DbContext, IRepository<Drug>>(guidesDbContext);
            controller.RegisterInstance<DbContext, IRepository<Drugform>>(guidesDbContext);



            // REPOSITORY SECTION
            //---------------------------------------------

            controller.RegisterService<IRepository<RecipeRow>, RecipeRepository>();
            controller.RegisterService<IRepository<RemainRow>, RemainRepository>();
            controller.RegisterService<IGuideRepository<Mnn>, MnnRepository>();
            controller.RegisterService<IGuideRepository<TradeName>, TradeNameRepository>();
            controller.RegisterService<IGuideRepository<Drug>, DrugRepository>();
            controller.RegisterService<IGuideRepository<Drugform>, DrugformRepository>();

            var logRepo = new LogRepository("logs/");
            controller.RegisterInstance<IRepository<LogRow>>(logRepo);


            var recipesWebServiceConfig = webServiceConfig.Get("RecipeServiceSoap");

            var recipesSoapClient = new RecipeServiceSoapClient(recipesWebServiceConfig.Name,
                recipesWebServiceConfig.RemoteAddress);

            controller.RegisterInstance<RecipeServiceSoap>(recipesSoapClient);

            var remainWebServiceConfig = webServiceConfig.Get("DrugstoreServiceSoap");

            var drugstoreSoapClient = new DrugstoreServiceSoapClient(remainWebServiceConfig.Name,
                remainWebServiceConfig.RemoteAddress);

            controller.RegisterInstance<DrugstoreServiceSoap>(drugstoreSoapClient);
        }



        // SERVICES CONFIGURE SECTION
        //---------------------------------------------------------------------

        private static void ConfigureServices(IApplicationController controller)
        {
            controller.RegisterService<EntityService<RemainRow>, RemainService>();
            controller.RegisterService<EntityService<RecipeRow>, RecipeService>();

            controller.RegisterService<IGuideService, GuideService>();
        }



        // VIEWS CONFIGURE SECTION
        //---------------------------------------------------------------------

        private static void ConfigureViews(IApplicationController controller)
        {
            controller.RegisterView<IConnectionsSettingsView, ConnectionsSettingsControl>();
            controller.RegisterView<IFtpSettingsView, FtpSettingsControl>();
            controller.RegisterView<ILogsView, LogsViewerControl>();
            controller.RegisterView<ISenderSettingsView, SenderSettingsControl>();
            controller.RegisterView<ITaskSettingsView, TaskSettingsControl>();
            controller.RegisterView<ISettingsCompositeView, SettingsForm>();
            
            controller.RegisterView<IMainView, MainForm>();
        }
    }
}
