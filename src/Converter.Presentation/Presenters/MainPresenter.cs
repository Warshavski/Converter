using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using log4net.Appender;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Entities.Guides;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Repositories.Exceptions;
using Escyug.Converter.Models.Services;
using Escyug.Converter.Models.Services.Exceptions;
using Escyug.Converter.Models.Utils;
using Escyug.Converter.Models.Utils.MailSender;
using Escyug.Converter.Models.Utils.MailSender.Exceptions;

using Escyug.Converter.Presentation.Common;
using Escyug.Converter.Presentation.Views;


namespace Escyug.Converter.Presentation.Presenters
{
    /** TODO :
     * 1. Decomposite functions for entities sending(remains, recipes)
     * 2. Add nolmal logging :
     *      - Info - for all log files
     *      - Error - for all log files
     *      - Fatal - only for rolling file
     */
    public class MainPresenter : BasePresenter<IMainView>
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly ILog _log;
        private readonly IMailSender _mailSender;

        private readonly IConfigurationManager<SenderConfiguration> _senderConfigManager;
        private readonly IConfigurationManager<FtpConfiguration> _ftpConfigManager;

        private readonly EntityService<RecipeRow> _recipeService;
        private readonly EntityService<RemainRow> _remainService;

        private readonly IRepository<RecipeRow> _recipeRepository;
        private readonly IRepository<RemainRow> _remainRepository;

        private readonly IGuideService _guideService;



        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        public MainPresenter(IMainView view, IApplicationController appController,
            IMainPresenterDependencyBlock dependencyBlock)
            : base(view, appController)
        {

            // UTILS BINDINGS SECTION
            //---------------------------------------------
            var logManager = dependencyBlock.LogManager;
            _log = logManager.GetLog(typeof(MainPresenter));

            _mailSender = dependencyBlock.MailSender;
            _senderConfigManager = dependencyBlock.SenderConfigManager;
            _ftpConfigManager = dependencyBlock.FtpConfigManager;

            // SERVICES BINDINGS SECTION
            //---------------------------------------------
            _recipeService = dependencyBlock.RecipeService;
            _remainService = dependencyBlock.RemainService;
            _guideService = dependencyBlock.GuideService;

            // REPOSITORIES BINDINGS SECTION
            //---------------------------------------------
            _recipeRepository = dependencyBlock.RecipeRepository;
            _remainRepository = dependencyBlock.RemainRepository;

            // EVENTS BINDINGS SECTION
            //---------------------------------------------
            View.InitializeAsync += OnInitializeAsync;
        }



        // EVENTS HANDLERS METHODS SECTION
        //---------------------------------------------------------------------
  
        //*** find better solution
        private async Task OnInitializeAsync()
        {
            View.IsBusy = true;

            var clientId = ConfigurationManager.AppSettings["storeId"];

            var ftpConfiguration = _ftpConfigManager.Get();
            var ftpClient = new FtpClient(ftpConfiguration.Uri, ftpConfiguration.Port,
                ftpConfiguration.User, ftpConfiguration.Password);

            var recipesFileName = ConfigurationManager.AppSettings["recipesFileName"];
            var remainsFileName = ConfigurationManager.AppSettings["remainsFileName"];

            try
            {
                await UpdateGuides(ftpClient);

                var guidesIdsCollection = await _guideService.GetGuidesIdsAsync();

                /** *** get test data
                 * 
                var recipesList = await _recipeRepository.GetAllAsync(recipesFileName);
                await ProcessRecipes(clientId, recipesList, guidesIdsCollection);
                */

                var remainsList = await _remainRepository.GetAllAsync(remainsFileName);
                await ProcessRemains(clientId, remainsList, guidesIdsCollection);
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
                View.Error = ex.Message;
            }
            catch (ArgumentException ex)
            {
                _log.Error(ex.ToString());
                View.Error = ex.Message;
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = ex.Message;
            }
            catch (RemoteServerException ex)
            {
                _log.Error(ex.ToString());
                View.Error = ex.Message;
            }
            catch (FileSaveException ex)
            {
                _log.Error(ex.ToString());
                View.Error = ex.Message;
            }

            View.Close();
        }



        // PRIVATE HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Private helper methods

        private async Task UpdateGuides(FtpClient ftpClient)
        {
            _log.Info("Проверка справочников на обновление");

            var metadata = _guideService.TryGetGuideFileMetadata(ftpClient);

            if (metadata != null)
            {
                _log.Info("Обновление справочников...");
                await _guideService.UpdateGuidesAsync(ftpClient, metadata);
            }

            _log.Info("Справочники обновлены");
        }

        private async Task ProcessRecipes(string clientId,
            List<RecipeRow> recipesCollection, GuidesIdsCollection guidesIdsCollection)
        {
            /** *** get test data
             * 
            var isRequiredFieldsValid =
                _recipeService.CheckRequiredFields(recipesCollection, guidesIdsCollection);

            if (isRequiredFieldsValid)
            {
                await SendEntitiesAsync(clientId, _recipeService, recipesCollection);
            }
            else
            {
                _log.Info("Записи из выгрузки рецептов отсутствуют в справочнике");
            }
            */
            await SendEntitiesAsync(clientId, _recipeService, recipesCollection);
        }

        private async Task ProcessRemains(string clientId,
            List<RemainRow> remainsCollection, GuidesIdsCollection guidesIdsCollection)
        {
            /** *** get test data
             * 
            var isRequiredFieldsValid =
                _remainService.CheckRequiredFields(remainsCollection, guidesIdsCollection);

            if (isRequiredFieldsValid)
            {
                await SendEntitiesAsync(clientId, _remainService, remainsCollection);
            }
            else
            {
                _log.Info("Записи из выгрузки остатков отсутствуют в справочнике");
            }
            */
            await SendEntitiesAsync(clientId, _remainService, remainsCollection);
        }

        private void SendEntities<TEntity>(string clientId,
            EntityService<TEntity> entitySendService, IList<TEntity> entitiList)
            where TEntity : class
        {
            var serviceResponse = entitySendService.SendData(entitiList, clientId);

            if (serviceResponse != null)
            {
                LogServiceResponse(serviceResponse);

                var rejectedXmlFile = entitySendService.CreateXmlFile(serviceResponse.RejectedBatch);
                var xmlFileName = serviceResponse.RejectedBatchName;
                rejectedXmlFile.Save("rejected/" + xmlFileName );

                var xmlFilePath = "rejected/" + xmlFileName;
                var logAppender = _log.Logger.Repository.GetAppenders()
                    .OfType<FileAppender>()
                    .FirstOrDefault();

                if (logAppender != null)
                {
                    var logFilePath = logAppender.File;

                    SendMailMessage(xmlFilePath, logFilePath, clientId);
                }
            }
            else
            {
                _log.Info("Send completed");
            }
        }

        private async Task SendEntitiesAsync<TEntity>(string clientId,
            EntityService<TEntity> entitySendService, IList<TEntity> entitiList)
            where TEntity : class
        {
            var serviceResponse = await entitySendService.SendDataAsync(entitiList, clientId);

            if (serviceResponse.RejectedBatch != null)
            {
                LogServiceResponse(serviceResponse);

                var xmlFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}/rejected/{serviceResponse.RejectedBatchName}.xml";
                try
                {
                    var rejectedXmlFile = entitySendService.CreateXmlFile(serviceResponse.RejectedBatch);
                    rejectedXmlFile.Save(xmlFilePath);
                }
                catch (IOException ex)
                {
                    _log.Info(ex.Message);
                    _log.Error(ex.ToString());
                }

                var logAppender = _log.Logger.Repository.GetAppenders()
                       .OfType<FileAppender>()
                       .FirstOrDefault();

                if (logAppender != null)
                {
                    var logFilePath = logAppender.File;

                   await SendMailMessageAsync(xmlFilePath, logFilePath, clientId);
                }
            }
            else
            {
                _log.Info("Загрузка выполнена");
            }
        }
        
        private void SendMailMessage(string xmlFilePath, string logFilePath, string clientId)
        {

            var senderConfig = _senderConfigManager.Get();

            try
            {
                _mailSender.Send(senderConfig.Host, senderConfig.Port,
                    new[] { senderConfig.SenderLogin, senderConfig.SenderPassword },
                    senderConfig.To, "Converter error", "Client ID : " + clientId,
                    new[] { xmlFilePath, logFilePath });
            }
            catch (MailSenderConfigurationException ex)
            {
                _log.Info(ex.Message);
                _log.Error(ex.ToString());
            }
            catch (MailAttachmentException ex)
            {
                _log.Info(ex.Message);
                _log.Error(ex.ToString());
            }
            catch (MailSenderException ex)
            {
                _log.Info(ex.Message);
                _log.Error(ex.ToString());
            }
        }

        private async Task SendMailMessageAsync(string xmlFilePath, string logFilePath, string clientId)
        {
            var senderConfig = _senderConfigManager.Get();

            try
            {
                await _mailSender.SendAsync(senderConfig.Host, senderConfig.Port,
                    new[] { senderConfig.SenderLogin, senderConfig.SenderPassword },
                    senderConfig.To, "Converter error", "Client ID : " + clientId,
                    new[] { xmlFilePath, logFilePath });
            }
            catch (MailSenderConfigurationException ex)
            {
                _log.Info(ex.Message);
                _log.Error(ex.ToString());
            }
            catch (MailAttachmentException ex)
            {
                _log.Info(ex.Message);
                _log.Error(ex.ToString());
            }
            catch (MailSenderException ex)
            {
                _log.Info(ex.Message);
                _log.Error(ex.ToString());
            }
        }

        private void LogServiceResponse<TEntity>(ServiceResponse<TEntity> serviceResponse)
        {
            _log.Info("Ошибка при загрузке на сервер");

            var errorStackBulder = new StringBuilder();
            foreach (var error in serviceResponse.WebServiceMessages)
            {
                errorStackBulder.AppendFormat("Id: {0} Message: {1}",
                    error.Id, error.Message);
            }

            _log.Error(errorStackBulder.ToString());
        }
        




        





        // *** NOT USED PROTOTYPE SECTION
        //-------------------------------------------------
        private async Task SendRemains(string clientId, List<RemainRow> remainsList,
            GuidesIdsCollection guidesIdsCollection) 
        {
            _log.Info("Checking remains in guides");
            var isRemainsFieldsValid =
                _remainService.CheckRequiredFields(remainsList, guidesIdsCollection);

            if (!isRemainsFieldsValid)
            {
                _log.Info("Remains check failed");
                _log.Info("Remains send failed");
                return;
            }

            _log.Info("Remains checked");

            _log.Info("Remains sending");
            //*** _remainService move to function parameters 
            await SendEntitiesAsync(clientId, _remainService, remainsList);
        }

        private async Task SendRecipes(string clientId, List<RecipeRow> recipesList,
            GuidesIdsCollection guidesIdsCollection)
        {
            _log.Info("Checking recipes in guides");
            var isRemainsFieldsValid =
                _recipeService.CheckRequiredFields(recipesList, guidesIdsCollection);

            if (!isRemainsFieldsValid)
            {
                _log.Info("Recipes check failed");
                _log.Info("Recipes send failed");
                return;
            }

            //*** _remainService move to function parameters 
            await SendEntitiesAsync(clientId, _recipeService, recipesList);
        }

        #endregion Private helper methods
    }
}