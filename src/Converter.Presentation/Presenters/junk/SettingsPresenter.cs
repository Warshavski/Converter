using System;
using System.Threading.Tasks;

using log4net;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Repositories.Exceptions;
using Escyug.Converter.Models.Utils.MailSender;

using Escyug.Converter.Presentation.Common;
using Escyug.Converter.Presentation.Views.junk;
using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Utils.MailSender.Exceptions;

namespace Escyug.Converter.Presentation.Presenters.junk
{
    public class SettingsPresenter : BasePresenter<ISettingsView>
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private const int UserDefineReceiverIndex = 0;

        private readonly IMailSender _mailSender;
        private readonly ILog _log;

        private readonly IConfigurationManager<SenderConfiguration> _senderConfigManager;
        private readonly IConfigurationManager<ConnectionStringConfiguration> _connectionConfigManager;
        private readonly IConfigurationManager<FtpConfiguration> _ftpConfigurationManager;
        private readonly IConfigurationManager<GuidesConfiguration> _guidesConfigurationManager;
        private readonly IConfigurationManager<ConverterTask> _taskConfigManager;
        private readonly IConfigurationManager<WebServiceConfiguration> _webServiceConfigurationManager;

        private readonly IRepository<LogRow> _logsRepository;



        // PRIVATE CLASS MEMBERS SECTION
        //---------------------------------------------------------------------

        private SenderConfiguration _senderConfig;
        private ConnectionStringConfiguration _recipesConnection;
        private ConnectionStringConfiguration _remainsConnection;

        private FtpConfiguration _ftpConfiguration;
        private GuidesConfiguration _guidesConfiguration;

        private WebServiceConfiguration _recipesWebServiceConfiguration;
        private WebServiceConfiguration _remainsWebServiceConfiguration;

        private ConverterTask _converterTask;

        
        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        public SettingsPresenter(ISettingsView view, IApplicationController appController,
            ISettingsPresenterDependencyBlock dependencyBlock)
            : base(view, appController)
        {

            // UTILS BINDING SECTION
            //---------------------------------------------
            _mailSender = dependencyBlock.MailSender;
            _log = dependencyBlock.LogManager.GetLog(typeof(SettingsPresenter));


            // REPOSITORIES BINDING SECTION
            //---------------------------------------------
            _senderConfigManager = dependencyBlock.SenderConfigurationManager;
            _connectionConfigManager = dependencyBlock.ConnectionConfigurationManager;
            _ftpConfigurationManager = dependencyBlock.FtpConfigurationManager;
            _guidesConfigurationManager = dependencyBlock.GuidesConfigurationManager;
            _webServiceConfigurationManager = dependencyBlock.WebServiceConfigurationManager;
            _taskConfigManager = dependencyBlock.TaskConfigurationManager;

            _logsRepository = dependencyBlock.LogsRepository;


            // EVENTS BINDING SECTION
            //---------------------------------------------
            View.InitializeView += OnInitializeView;
            View.SaveSenderSettings += OnSaveSenderSettings;
            View.SaveConnectionsSettings += OnSaveConnectionsSettings;
            View.SaveGuidesSettings += OnSaveGuidesSettings;
            View.SaveTaskSettings += OnSaveTaskSettings;
            View.TestSenderAsync += OnTestSenderAsync;
        }



        // ON EVENTS SUBSCRIBERS SECTION
        //---------------------------------------------------------------------

        #region On events subscribers

        private void OnInitializeView()
        {
            LoadConnectionsConfiguration();
            LoadFtpConfiguration();
            LoadSenderConfiguration();
            LoadTaskConfiguration();
            LoadWebServiceConfiguration();
            LoadLogs();
        }

        private void OnSaveSenderSettings()
        {
            if (string.IsNullOrEmpty(View.SenderHost))
            {
                View.Error = "Sender host can't be empty";
                return;
            }

            if (string.IsNullOrEmpty(View.SenderLogin))
            {
                View.Error = "Sender login can't be empty";
                return;
            }

            if (string.IsNullOrEmpty(View.SenderPassword))
            {
                View.Error = "Sender password can't be empty";
                return;
            }

            if (string.IsNullOrEmpty(View.SenderReceiver))
            {
                View.Error = "Sender receiver can't be empty";
                return;
            }
            
            _senderConfig.Host = View.SenderHost;
            _senderConfig.Port = View.SenderPort;

            _senderConfig.SenderLogin = View.SenderLogin;
            _senderConfig.SenderPassword = View.SenderPassword;

            _senderConfig.To[UserDefineReceiverIndex] = View.SenderReceiver;

            try
            {
                _senderConfigManager.Update(_senderConfig);

                View.Notify = "Email sender settings saved";
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Sender configuration not set";
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Can't save sender settings data" +
                             Environment.NewLine +
                             ex.Message;
            }
            
        }

        private async Task OnTestSenderAsync()
        {
            View.IsBusy = true;

            //*** exception handling
            await Task.Run(() =>
            {
                try
                {
                    var host = View.SenderHost;
                    var port = View.SenderPort;
                    var from = new[]
                    {
                        View.SenderLogin,
                        View.SenderPassword
                    };

                    var to = new[] {View.SenderReceiver};

                    _mailSender.Send(host, port, from, to, "Sender test", "Test message", new string[] {});

                    View.Notify = "Test email was sent";
                }
                catch (ArgumentNullException ex)
                {
                    View.Error = "Mail sender error :" +
                                 Environment.NewLine +
                                 ex.Message;

                    _log.Error(ex.ToString());
                }
                catch (ArgumentException ex)
                {
                    View.Error = "Mail sender error :" +
                                 Environment.NewLine +
                                 ex.Message;

                    _log.Error(ex.ToString());
                }
                catch (MailAttachmentException ex)
                {
                    View.Error = "Mail sender error :" +
                                 Environment.NewLine +
                                 ex.Message;

                    _log.Error(ex.ToString());
                }
                catch (MailSenderException ex)
                {
                    View.Error = "Mail sender error :" +
                                 Environment.NewLine +
                                 ex.Message;

                    _log.Error(ex.ToString());
                }
                finally
                {
                    View.IsBusy = false;
                }
            });
        }

        private void OnSaveConnectionsSettings()
        {
            SaveEntityFoldersConfiguration();
            SaveWebServiceConfiguration();
        }

        private void OnSaveGuidesSettings()
        {
            SaveFtpConfiguration();
            SaveGuidesConfiguration();
        }

        private void OnSaveTaskSettings()
        {
            if (View.TaskHours > 24 || View.TaskHours <= 0)
            {
                View.Error = "Hours should be between 1 and 24";
                return;
            }

            if (View.TaskMinutes > 60 || View.TaskMinutes < 0)
            {
                View.Error = "Minutes should be between 0 and 60";
                return;
            }

            _converterTask.Hours = View.TaskHours;
            _converterTask.Minutes = View.TaskMinutes;

            try
            {
                _taskConfigManager.Update(_converterTask);

                View.Notify = "Task settings saved";
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Task configuration not set";
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Can't save task settings data" +
                             Environment.NewLine +
                             ex.Message;
            }
        }

        #endregion On events subscribers



        // HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Helper methods

        // LOAD CONFIGURATIONS SECTION
        //-------------------------------------------------
        private void LoadConnectionsConfiguration()
        {
            // RECIPES CONNECTION
            //---------------------------------------------
            try
            {
                _recipesConnection = _connectionConfigManager.Get("recipes");
                View.RecipesFolderPath = _recipesConnection.ConnectionString;
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
            }


            // REMAINS CONNECTION
            //---------------------------------------------
            try
            {
                _remainsConnection = _connectionConfigManager.Get("remains");
                View.RemainsFolderPath = _remainsConnection.ConnectionString;
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
            }


            // GUIDES CONNECTION
            //---------------------------------------------
            try
            {
                _guidesConfiguration = _guidesConfigurationManager.Get();
                View.GuidesFolderPath = _guidesConfiguration.Path;
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
            }
        }

        private void LoadTaskConfiguration()
        {
            try
            {
                _converterTask = _taskConfigManager.Get(ConverterTask.DefaultTaskName);
                if (_converterTask == null)
                {
                    var appPath = AppDomain.CurrentDomain.BaseDirectory;
                    var appName = AppDomain.CurrentDomain.FriendlyName;

                    var fullAppPath = System.IO.Path.Combine(appPath, appName);

                    _converterTask= new ConverterTask
                    {
                        Name = ConverterTask.DefaultTaskName,
                        Parameters = ConverterTask.DefaultTaskArguments,
                        Hours = ConverterTask.DefaultTriggerHours,
                        Minutes = ConverterTask.DefaultTriggerMinutes,
                        Path = fullAppPath
                    };

                    _taskConfigManager.Set(_converterTask);
                }

                View.TaskName = _converterTask.Name;
                View.TaskHours = _converterTask.Hours;
                View.TaskMinutes = _converterTask.Minutes;
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Error on task configuration load :" +
                             Environment.NewLine +
                             ex.Message;
            }
            catch (ArgumentException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Error on task configuration load :" +
                             Environment.NewLine +
                             ex.Message;
            }
        }

        private void LoadSenderConfiguration()
        {
            try
            {
                _senderConfig = _senderConfigManager.Get();

                View.SenderHost = _senderConfig.Host;
                View.SenderPort = _senderConfig.Port;
                View.SenderLogin = _senderConfig.SenderLogin;
                View.SenderPassword = _senderConfig.SenderPassword;

                if (_senderConfig.To.Length == 0)
                {
                    View.Error = "Can't load sender receivers";
                }
                else
                {
                    View.SenderReceiver = _senderConfig.To[UserDefineReceiverIndex];
                }
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Error on sender configuration load :" +
                             Environment.NewLine +
                             ex.Message;
            }
        }

        private void LoadFtpConfiguration()
        {
            try
            {
                _ftpConfiguration = _ftpConfigurationManager.Get();

                View.FtpHost = _ftpConfiguration.Uri;
                View.FtpPort = _ftpConfiguration.Port;
                View.FtpUser = _ftpConfiguration.User;
                View.FtpPassword = _ftpConfiguration.Password;
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Error on ftp configuration load :" +
                             Environment.NewLine +
                             ex.Message;
            }
            
        }

        private void LoadWebServiceConfiguration()
        {
            try
            {
                _recipesWebServiceConfiguration = _webServiceConfigurationManager.Get("RecipeServiceSoap");
                _remainsWebServiceConfiguration = _webServiceConfigurationManager.Get("DrugstoreServiceSoap");

                if (_recipesWebServiceConfiguration != null)
                {
                    View.RecipesServiceAddress = _recipesWebServiceConfiguration.RemoteAddress;
                }

                if (_remainsWebServiceConfiguration != null)
                {
                    View.RemainsServiceAddress = _remainsWebServiceConfiguration.RemoteAddress;
                }
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Can't load web service connection strings" +
                             Environment.NewLine +
                             ex.Message;
            }
            
        }

        private void LoadLogs()
        {
            try
            {
                var logs = _logsRepository.GetAll("user.log");
                View.LogsList = logs;
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Error on application logs load :" +
                             Environment.NewLine +
                             ex.Message;
            }
        }


        // SAVE CONFIGURATIONS SECTION
        //-------------------------------------------------
        private void SaveWebServiceConfiguration()
        {
            if (string.IsNullOrEmpty(View.RecipesServiceAddress))
            {
                View.Error = "Recipes web service address can't be empty";
                return;
            }

            if (string.IsNullOrEmpty(View.RemainsServiceAddress))
            {
                View.Error = "Remains web serive address can't be empty";
                return;
            }

            try
            {
                _recipesWebServiceConfiguration.RemoteAddress = View.RecipesServiceAddress;
                _webServiceConfigurationManager.Update(_recipesWebServiceConfiguration);

                _remainsWebServiceConfiguration.RemoteAddress = View.RemainsServiceAddress;
                _webServiceConfigurationManager.Update(_remainsWebServiceConfiguration);
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Configutation not set";
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Can't save web service configuration data" +
                             Environment.NewLine +
                             ex.Message;
            }
        }

        private void SaveEntityFoldersConfiguration()
        {
            if (string.IsNullOrEmpty(View.RecipesFolderPath))
            {
                View.Error = "Recipes folder field can't be empty";
                return;
            }

            if (string.IsNullOrEmpty(View.RemainsFolderPath))
            {
                View.Error = "Remains folder field can't be empty";
                return;
            }

            try
            {
                _recipesConnection.ConnectionString = View.RecipesFolderPath;
                _connectionConfigManager.Update(_recipesConnection);

                _remainsConnection.ConnectionString = View.RemainsFolderPath;
                _connectionConfigManager.Update(_remainsConnection);
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Configutation not set";
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Can't save connection strings data" +
                             Environment.NewLine +
                             ex.Message;
            }

        }

        private void SaveFtpConfiguration()
        {
            if (string.IsNullOrEmpty(View.FtpUser))
            {
                View.Error = "Ftp user name can't be empty";
                return;
            }

            if (string.IsNullOrEmpty(View.FtpPassword))
            {
                View.Error = "Ftp password can't be empty";
                return;
            }

            if (string.IsNullOrEmpty(View.FtpHost))
            {
                View.Error = "Ftp host can't be empty";
            }

            _ftpConfiguration.Port = View.FtpPort;
            _ftpConfiguration.Uri = View.FtpHost;
            _ftpConfiguration.User = View.FtpUser;
            _ftpConfiguration.Password = View.FtpPassword;

            try
            {
                _ftpConfigurationManager.Update(_ftpConfiguration);
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Ftp configuration not set";
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Can't save ftp settings data" +
                             Environment.NewLine +
                             ex.Message;
            }
        }

        private void SaveGuidesConfiguration()
        {
            if (string.IsNullOrEmpty(View.GuidesFolderPath))
            {
                View.Error = "Guides folder path can't be empty";
                return;
            }

            _guidesConfiguration.Path = View.GuidesFolderPath;

            try
            {
                _guidesConfigurationManager.Update(_guidesConfiguration);
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Guides configuration not set";
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Can't save guides settings data" +
                             Environment.NewLine +
                             ex.Message;
            }
        }

        #endregion Helper methods
    }
}
