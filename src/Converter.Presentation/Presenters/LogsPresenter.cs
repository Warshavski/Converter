using System;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Repositories.Exceptions;

using Escyug.Converter.Presentation.Common;
using Escyug.Converter.Presentation.Views;

namespace Escyug.Converter.Presentation.Presenters
{
    public class LogsPresenter : BasePresenter<ILogsView>
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly IRepository<LogRow> _logsRepository;



        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------
        
        public LogsPresenter(ILogsView view, IApplicationController appController, 
            IRepository<LogRow> logsRepository)
            : base(view, appController)
        {
            _logsRepository = logsRepository;


            // EVENTS BINDINGS SECTION
            //---------------------------------------------
            View.InitializeView += () => OnInitializeView();
        }



        // ON EVENTS SUBSCRIBERS SECTION
        //---------------------------------------------------------------------

        private void OnInitializeView()
        {
            try
            {
                var logs = _logsRepository.GetAll("user.log");
                View.LogsList = logs;
            }
            catch (RepositoryLoadException ex)
            {
                View.Error = "Не удалось загрузить логи :" +
                             Environment.NewLine +
                             ex.Message;
            }
        }
    }
}
