using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.Converter.Presentation.Common;
using Escyug.Converter.Models.Entities;

namespace Escyug.Converter.Presentation.Views.junk
{
    public interface ISettingsView : IView
    {
        // SYNC EVENTS SECTION
        //---------------------------------------------------------------------

        event Action InitializeView;
        event Action SaveSenderSettings;
        event Action SaveConnectionsSettings;
        event Action SaveGuidesSettings;
        event Action SaveTaskSettings;



        // ASYNC EVENTS SECTION
        //---------------------------------------------------------------------

        event Func<Task> TestSenderAsync;



        // PROPERTIES SECTION
        //---------------------------------------------------------------------

        string RecipesServiceAddress { get; set; }
        string RemainsServiceAddress { get; set; }

        string SenderHost { get; set; }
        int SenderPort { get; set; }
        string SenderPassword { get; set; }
        string SenderLogin { get; set; }
        string SenderReceiver { get; set; }

        string RemainsFolderPath { get; set; }
        string RecipesFolderPath { get; set; }
        string GuidesFolderPath { get; set; }

        string FtpHost { get; set; }
        int FtpPort { get; set; }
        string FtpUser { get; set; }
        string FtpPassword { get; set; }

        string TaskName { get; set; }
        short TaskHours { get; set; }
        short TaskMinutes { get; set; }

        ICollection<LogRow> LogsList { set; }

        bool IsBusy { get; set; }

        //*** drugstore id (credentials)
        
    }
}
