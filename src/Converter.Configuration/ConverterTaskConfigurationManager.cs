using System;

//using TaskScheduler;
using Microsoft.Win32.TaskScheduler;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Repositories;

namespace Escyug.Converter.Configuration
{
    public class ConverterTaskConfigurationManager : IConfigurationManager<ConverterTask>
    {

        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        #region IConfigurationManager<ConverterTask>

        //*** check if scheduledConverterTask  is null
        public ConverterTask Get(string taskName)
        {
            if (string.IsNullOrEmpty(taskName))
            {
                throw  new ArgumentNullException(nameof(taskName));
            }

            using (var taskService = new TaskService())
            {
                Task schedulledTask = taskService.FindTask(taskName);

                if (schedulledTask == null)
                {
                    return null;
                }
                    
                var taskDefinition = schedulledTask.Definition;
                var taskActionArguments = (taskDefinition.Actions[0] as ExecAction)?.Arguments;

                short triggerHours = 0;
                short triggerMinutes = 0;

                var taskTrigger = taskDefinition.Triggers[0] as DailyTrigger;
                if (taskTrigger != null)
                {
                    triggerHours = (short) taskTrigger.StartBoundary.Hour;
                    triggerMinutes = (short) taskTrigger.StartBoundary.Minute;
                }
                
                var converterTask = new ConverterTask
                {
                    Name = schedulledTask.Name,
                    Parameters = taskActionArguments,
                    Hours = triggerHours,
                    Minutes = triggerMinutes
                };

                return converterTask;
            }
            /** using TaskScheduler;
            using (var scheduledTasks = new ScheduledTasks())
            {
                //scheduledTasks.DeleteTask(name);

                var scheduledConverterTask = scheduledTasks.OpenTask(name);

                var trigger = scheduledConverterTask?.Triggers[0] as StartableTrigger;

                if (trigger == null)
                {
                    return null;
                }
                
                var converterTask = new ConverterTask
                {
                    Name = scheduledConverterTask.Name,
                    Path = scheduledConverterTask.ApplicationName,
                    Parameters = scheduledConverterTask.Parameters,
                    Hours = trigger.StartHour,
                    Minutes = trigger.StartMinute
                };

                return converterTask;
            }
            */
        }

        public void Update(ConverterTask task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            using (var taskService = new TaskService())
            {
                var schedulledTask = taskService.FindTask(task.Name);

                if (schedulledTask == null)
                {
                    throw  new ArgumentException(nameof(schedulledTask));
                }

                var schedulledTaskTrigger = schedulledTask.Definition.Triggers[0] as DailyTrigger;
                if (schedulledTaskTrigger == null)
                {
                    schedulledTask.Definition.Triggers.Add(new DailyTrigger
                    {
                        StartBoundary = DateTime.Today +
                        TimeSpan.FromHours(task.Hours) +
                        TimeSpan.FromMinutes(task.Minutes)
                    });
                }
                else
                {
                    schedulledTaskTrigger.StartBoundary = DateTime.Today +
                                                          TimeSpan.FromHours(task.Hours) +
                                                          TimeSpan.FromMinutes(task.Minutes);
                }

                schedulledTask.RegisterChanges();
            }

            /** using TaskScheduler;
            var taskName = task.Name;
            using (var scheduledTasks = new ScheduledTasks())
            {
                var scheduledConverterTask = scheduledTasks.OpenTask(taskName);

                if (scheduledConverterTask == null)
                {
                    throw new ArgumentException(nameof(scheduledConverterTask));
                }
                
                var trigger = scheduledConverterTask.Triggers[0] as StartableTrigger;
                if (trigger == null)
                {
                    scheduledConverterTask.Triggers.Add(
                        new DailyTrigger(task.Hours, task.Minutes));
                }
               
                scheduledConverterTask.Save();
            }
            */
        }

        //*** NOT USED
        public ConverterTask Get()
        {
            throw new NotImplementedException();
        }
        
        public void Set(ConverterTask task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            // Get the service on the local machine
            using (var taskService = new TaskService())
            {
                // Create a new task definition and assign properties
                TaskDefinition taskDefinition = taskService.NewTask();
                taskDefinition.RegistrationInfo.Description = "Processing center converter autostart";
                taskDefinition.Principal.LogonType = TaskLogonType.InteractiveToken;

                // Add a trigger that will fire every day at time
                taskDefinition.Triggers.Add(new DailyTrigger
                {
                    StartBoundary = DateTime.Today + 
                        TimeSpan.FromHours(task.Hours) +
                        TimeSpan.FromMinutes(task.Minutes)     
                });
                
                // Add an action that will launch programm whenever the trigger fires
                taskDefinition.Actions.Add(new ExecAction(task.Path, task.Parameters, null));

                // Register the task in the root folder
                const string taskName = "Processing center converter";
                taskService.RootFolder.RegisterTaskDefinition(taskName, taskDefinition);
            }

            /** using TaskScheduler;
            //Get a ScheduledTasks object for the local computer.
            using (var scheduledTasks = new ScheduledTasks())
            {
                // Create a task
                //*** throws argument exeption
                using (var converterTask = scheduledTasks.CreateTask(task.Name))
                {
                    string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                    //*** Fill in the program info
                    converterTask.Creator = userName;
                    converterTask.ApplicationName = task.Path;
                    converterTask.Parameters = task.Parameters;
                    converterTask.Comment = "Runs processing center converter by schedule";

                    // Set the account under which the task should run.
                    converterTask.SetAccountInformation(string.Empty, (string)null);

                    // Declare that the system must have been idle for ten minutes before 
                    // the task will start
                    converterTask.IdleWaitMinutes = 0;

                    // Allow the task to run for no more than 2 hours, 30 minutes.
                    converterTask.MaxRunTime = new TimeSpan(2, 30, 0);

                    // Set priority to only run when system is idle.
                    converterTask.Priority = System.Diagnostics.ProcessPriorityClass.Normal;

                    // Create a trigger to start the task every day 
                    converterTask.Triggers.Add(new DailyTrigger(task.Hours, task.Minutes));

                    

                    // Save the changes that have been made.
                    converterTask.Save();

                    // Dispose the ScheduledTasks to release its COM resources.
                    //converterTask.Close();
                }
            }
            */
        }

        #endregion IConfigurationManager<ConverterTask>
    }
}

