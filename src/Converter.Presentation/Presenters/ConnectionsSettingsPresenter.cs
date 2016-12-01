using System;

using log4net;

using Escyug.Converter.Common.Logging;

using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Repositories.Exceptions;

using Escyug.Converter.Presentation.Common;
using Escyug.Converter.Presentation.Views;

namespace Escyug.Converter.Presentation.Presenters
{
    public class ConnectionsSettingsPresenter : BasePresenter<IConnectionsSettingsView>
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly ILog _log;
        private readonly IConfigurationManager<ConnectionStringConfiguration> _connectionConfigManager;
        private readonly IConfigurationManager<WebServiceConfiguration> _webServiceConfigurationManager;



        // CLASS FIELDS SECTION
        //---------------------------------------------------------------------

        private ConnectionStringConfiguration _recipesConnection;
        private ConnectionStringConfiguration _remainsConnection;

        private WebServiceConfiguration _recipesWebServiceConfiguration;
        private WebServiceConfiguration _remainsWebServiceConfiguration;



        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------

        //*** remove dependencies as much as possible
        public ConnectionsSettingsPresenter(IConnectionsSettingsView view, 
            IApplicationController appController, ILogManager logManager,
            IConfigurationManager<ConnectionStringConfiguration> connectionConfigManager,
            IConfigurationManager<WebServiceConfiguration> webServiceConfigurationManager) 
            : base(view, appController)
        {
            _log = logManager.GetLog(typeof(ConnectionsSettingsPresenter));

            _connectionConfigManager = connectionConfigManager;
            _webServiceConfigurationManager = webServiceConfigurationManager;


            // EVENTS BINDINGS SECTION
            //---------------------------------------------
            View.InitializeView += () => OnInitializeView();
            View.Save += () => OnSave();
        }



        // ON EVENTS SUBSCRIBERS SECTION
        //---------------------------------------------------------------------

        private void OnInitializeView()
        {
            LoadConnectionsConfiguration();
            LoadWebServiceConfiguration();
        }

        private void OnSave()
        {
            try
            {
                SaveEntityFoldersConfiguration();
                SaveWebServiceConfiguration();

                View.Notify = "Настройки подключений сохранены";
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Настройки не установлены";
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Не удалось сохранить настройки подключений" +
                             Environment.NewLine +
                             ex.Message;
            }
        }



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

                var folderPath = ParseFolderPath(_recipesConnection.ConnectionString);

                View.RecipesFolderPath = folderPath;

                /** Provider =Microsoft.Jet.OLEDB.4.0;
                 * Data Source=C:\test\converter\recipes\;
                 * User ID=Admin;
                 * Password=;
                 * Extended Properties='dBASE IV';*/
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

                var folderPath = ParseFolderPath(_remainsConnection.ConnectionString);

                View.RemainsFolderPath = folderPath;
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
                View.Error = "Невозможно загрузить настройки подключения к веб-сервисам" +
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
                View.Error = "Адрес веб-сервиса рецептов не может быть пустым";
                return;
            }

            if (string.IsNullOrEmpty(View.RemainsServiceAddress))
            {
                View.Error = "Адрес веб-сервиса остатков не может быть пустым";
                return;
            }

            _recipesWebServiceConfiguration.RemoteAddress = View.RecipesServiceAddress;
            _webServiceConfigurationManager.Update(_recipesWebServiceConfiguration);

            _remainsWebServiceConfiguration.RemoteAddress = View.RemainsServiceAddress;
            _webServiceConfigurationManager.Update(_remainsWebServiceConfiguration);
        }

        private void SaveEntityFoldersConfiguration()
        {
            if (string.IsNullOrEmpty(View.RecipesFolderPath))
            {
                View.Error = "Путь к папке с выгрузкой рецептов не может быть пустым";
                return;
            }

            if (string.IsNullOrEmpty(View.RemainsFolderPath))
            {
                View.Error = "Путь к папке с выгрузкой остатков не может быть пустым";
                return;
            }

            _recipesConnection.ConnectionString = CreateConnectionString(View.RecipesFolderPath);
            _connectionConfigManager.Update(_recipesConnection);

            _remainsConnection.ConnectionString = CreateConnectionString(View.RemainsFolderPath);
            _connectionConfigManager.Update(_remainsConnection);

        }


        // CONNECTION STRING MANIPULATION SECTION
        //-------------------------------------------------
        //*** rework this method
        private string ParseFolderPath(string connectionString)
        {
            const int removeItemIndex = 1;

            var connectionItems = connectionString.Split(';');

            var removeItemString = "Data Source=";

            int index = connectionItems[removeItemIndex].IndexOf(removeItemString);
            string folderPath = (index < 0)
                ? connectionItems[1]
                : connectionItems[1].Remove(index, removeItemString.Length);

            return folderPath;
        }

        private string CreateConnectionString(string folderPath)
        {
            var connectionString =
                $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={folderPath};User ID=Admin;Password=;Extended Properties='dBASE IV';";

            return connectionString;
        }

        #endregion Helper methods
    }
}
