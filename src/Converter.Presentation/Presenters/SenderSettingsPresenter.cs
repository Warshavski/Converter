using System;
using Escyug.Converter.Common.Logging;
using log4net;

using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Repositories.Exceptions;

using Escyug.Converter.Presentation.Common;
using Escyug.Converter.Presentation.Views;

namespace Escyug.Converter.Presentation.Presenters
{
    public class SenderSettingsPresenter : BasePresenter<ISenderSettingsView>
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private const int UserDefineReceiverIndex = 0;

        private readonly ILog _log;

        private readonly IConfigurationManager<SenderConfiguration> _senderConfigManager;



        // CLASS FIELDS SECTION
        //---------------------------------------------------------------------

        private SenderConfiguration _senderConfig;



        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------

        public SenderSettingsPresenter(ISenderSettingsView view, IApplicationController appController,
            IConfigurationManager<SenderConfiguration> senderConfigManager, ILogManager logManager)
            : base(view, appController)
        {
            _log = logManager.GetLog(typeof(SenderSettingsPresenter));
            _senderConfigManager = senderConfigManager;


            // EVENTS BINDINGS SECTION
            //---------------------------------------------
            View.InitializeView += () => OnInitializeView();
            View.Save += () => OnSave();
        }



        // ON EVENTS SUBSCRIBERS SECTION
        //---------------------------------------------------------------------

        #region  On events subscribers

        private void OnInitializeView()
        {
            try
            {
                _senderConfig = _senderConfigManager.Get();

                View.Host = _senderConfig.Host;
                View.Port = _senderConfig.Port;
                View.Login = _senderConfig.SenderLogin;
                View.Password = _senderConfig.SenderPassword;

                if (_senderConfig.To.Length == 0)
                {
                    View.Error = "Can't load sender receivers";
                }
                else
                {
                    View.Receiver = _senderConfig.To[UserDefineReceiverIndex];
                }
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Не удалось загрузить настройки SMTP-клиента :" +
                             Environment.NewLine +
                             ex.Message;
            }
        }

        private void OnSave()
        {
            if (string.IsNullOrEmpty(View.Host))
            {
                View.Error = "Поле Host не может быть пустым";
                return;
            }

            if (string.IsNullOrEmpty(View.Login))
            {
                View.Error = "Поле Login не может быть пустым";
                return;
            }

            if (string.IsNullOrEmpty(View.Password))
            {
                View.Error = "Поле Password не может быть пустым";
                return;
            }

            if (string.IsNullOrEmpty(View.Receiver))
            {
                View.Error = "Поле Receiver не может быть пустым";
                return;
            }

            _senderConfig.Host = View.Host;
            _senderConfig.Port = View.Port;

            _senderConfig.SenderLogin = View.Login;
            _senderConfig.SenderPassword = View.Password;

            _senderConfig.To[UserDefineReceiverIndex] = View.Receiver;

            try
            {
                _senderConfigManager.Update(_senderConfig);

                View.Notify = "Настройки сохранены";
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Sender configuration not set";
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Не удалось сохранить настройки" +
                             Environment.NewLine +
                             ex.Message;
            }
        }

        #endregion  On events subscribers
    }
}
