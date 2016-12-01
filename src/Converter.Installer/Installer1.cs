using System;
using System.Collections;
using System.ComponentModel;

using Microsoft.Win32.TaskScheduler;

namespace Escyug.Converter.Installer
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private const string TaskName = "Processing center converter";
        private const string TaskArguments = "-noui";

        private const int DefaultTriggerHour = 10;
        private const int DefaultTriggerMinutes = 20;



        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        public Installer1()
        {
            InitializeComponent();
        }



        // * OVERRIDED * PUBLIC METHODS SECTION
        //---------------------------------------------------------------------

        #region System.Configuration.Install.Installer members

        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }

        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);

            // Get the service on the local machine
            using (var taskService = new TaskService())
            {
                // Create a new task definition and assign properties
                TaskDefinition taskDefinition = taskService.NewTask();
                taskDefinition.RegistrationInfo.Description = "Processing center converter autostart";
                taskDefinition.Principal.LogonType = TaskLogonType.InteractiveToken;

                // Add a trigger that will fire every day at default time 
                taskDefinition.Triggers.Add(new DailyTrigger
                {
                    StartBoundary = DateTime.Today +
                        TimeSpan.FromHours(DefaultTriggerHour) +
                        TimeSpan.FromMinutes(DefaultTriggerMinutes)
                });

                var appPath = Context.Parameters["targetdir"];
                var appName = Context.Parameters["targetapp"];

                var fullAppPath = appPath.Remove(appPath.Length - 1, 1) + appName;
                    //System.IO.Path.Combine(appPath, appName);

                // Add an action that will launch programm whenever the trigger fires
                taskDefinition.Actions.Add(new ExecAction(fullAppPath, TaskArguments, null));

                // Register the task in the root folder
                taskService.RootFolder.RegisterTaskDefinition(TaskName, taskDefinition);
            }
        }

        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);

            using (var taskService = new TaskService())
            {
                var task = taskService.FindTask(TaskName);
                if (task != null)
                {
                    taskService.RootFolder.DeleteTask(TaskName);
                }
            }
        }

        #endregion System.Configuration.Install.Installer members
    }
}
