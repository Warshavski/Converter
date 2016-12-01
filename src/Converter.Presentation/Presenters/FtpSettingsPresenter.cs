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
    public class FtpSettingsPresenter : BasePresenter<IFtpSettingsView>
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly ILog _log;

        private readonly IConfigurationManager<FtpConfiguration> _ftpConfigurationManager;
        


        // CLASS FIELDS SECTION
        //---------------------------------------------------------------------
        private FtpConfiguration _ftpConfiguration;
        

        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------

        public FtpSettingsPresenter(IFtpSettingsView view, IApplicationController appController, 
            ILogManager logManager, IConfigurationManager<FtpConfiguration> ftpConfigurationManager)
            :base(view, appController)
        {
            _log = logManager.GetLog(typeof(FtpSettingsPresenter));

            _ftpConfigurationManager = ftpConfigurationManager;
            

            // EVENTS BINDINGS SECTION
            //---------------------------------------------
            View.Save += () => OnSave();
            View.InitializeView += () => OnInitializeView();
        }



        // ON EVENTS SUBSCRIBERS SECTION
        //---------------------------------------------------------------------

        #region On events subscribers

        private void OnInitializeView()
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

        private void OnSave()
        {
            if (string.IsNullOrEmpty(View.FtpHost))
            {
                View.Error = "Поле 'Host' не может быть пустым";
            }

            _ftpConfiguration.Port = View.FtpPort;
            _ftpConfiguration.Uri = CreateHostUrl(View.FtpHost);
            _ftpConfiguration.User = View.FtpUser;
            _ftpConfiguration.Password = View.FtpPassword;

            try
            {
                _ftpConfigurationManager.Update(_ftpConfiguration);
                View.Notify = "Настройки FTP сохранены";
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Настройки FTP не установлены";
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Не удалось сохранить настройки FTP" +
                             Environment.NewLine +
                             ex.Message;
            }
        }

        #endregion On events subscribers

        private string CreateHostUrl(string url)
        {
            var lastLetterIndex = url.Length - 1;
            if (!url[lastLetterIndex].Equals('/'))
            {
                return url + "/";
            }

            return url;
        }
    }
}
