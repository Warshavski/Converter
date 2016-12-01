using System;

using log4net;

using Escyug.Converter.Common.Logging;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Repositories.Exceptions;

using Escyug.Converter.Presentation.Common;
using Escyug.Converter.Presentation.Views;

namespace Escyug.Converter.Presentation.Presenters
{
    public class TaskSettingsPresenter : BasePresenter<ITaskSettingsView>
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly ILog _log;

        private readonly IConfigurationManager<ConverterTask> _taskConfigurationManager;



        // CLASS FIELDS SECTION
        //---------------------------------------------------------------------

        private ConverterTask _converterTask;



        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        public TaskSettingsPresenter(ITaskSettingsView view, IApplicationController appController,
            ILogManager logManager, IConfigurationManager<ConverterTask> taskConfigurationManager)
            : base(view, appController)
        {
            _log = logManager.GetLog(typeof(TaskSettingsPresenter));

            _taskConfigurationManager = taskConfigurationManager;


            // EVENTS BINDING SECTION
            //---------------------------------------------
            View.InitializeView += () => OnInitializeView();
            View.Save += () => OnSave();
        }



        // ON EVENTS SUBSCRIBERS SECTION
        //---------------------------------------------------------------------

        private void OnInitializeView()
        {
            try
            {
                _converterTask = _taskConfigurationManager.Get(ConverterTask.DefaultTaskName);
                if (_converterTask == null)
                {
                    var appPath = AppDomain.CurrentDomain.BaseDirectory;
                    var appName = AppDomain.CurrentDomain.FriendlyName;

                    var fullAppPath = System.IO.Path.Combine(appPath, appName);

                    _converterTask = new ConverterTask
                    {
                        Name = ConverterTask.DefaultTaskName,
                        Parameters = ConverterTask.DefaultTaskArguments,
                        Hours = ConverterTask.DefaultTriggerHours,
                        Minutes = ConverterTask.DefaultTriggerMinutes,
                        Path = fullAppPath
                    };

                    _taskConfigurationManager.Set(_converterTask);
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
        
        private void OnSave()
        {
            if (View.TaskHours > 24 || View.TaskHours <= 0)
            {
                View.Error = "Часы должны быть в диапазоне от 1 до 24";
                return;
            }

            if (View.TaskMinutes > 60 || View.TaskMinutes < 0)
            {
                View.Error = "Минуты должны быть в диапазоне от 0 до 60";
                return;
            }

            _converterTask.Hours = View.TaskHours;
            _converterTask.Minutes = View.TaskMinutes;

            try
            {
                _taskConfigurationManager.Update(_converterTask);

                View.Notify = "Настройки сохранены";
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Task configuration not set";
            }
            catch (RepositoryLoadException ex)
            {
                _log.Error(ex.ToString());
                View.Error = "Не удалось сохранить настройки" +
                             Environment.NewLine +
                             ex.Message;
            }
        }
    }
}
