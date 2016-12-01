using System;
using System.Windows.Forms;

using Escyug.Converter.App.WinForms.Forms;
using Escyug.Converter.Presentation.Views;

namespace Escyug.Converter.App.WinForms.UserControls
{
    public partial class TaskSettingsControl : SettingsControl, ITaskSettingsView
    {
        public TaskSettingsControl(ApplicationContext context)
            : base (context)
        {
            InitializeComponent();

            buttonSaveTaskSettings.Click += (sender, e) => Invoker.Invoke(Save);
        }

        public new void Show()
        {
            (Context.MainForm as SettingsForm)?.tabPageTask.Controls.Add(this);
        }


        #region ITaskSettingsView members

        public short TaskHours
        {
            get { return short.Parse(maskedTextBoxTaskHours.Text.Trim()); }
            set { maskedTextBoxTaskHours.Text = value.ToString(); }
        }

        public short TaskMinutes
        {
            get { return short.Parse(maskedTextBoxTaskMinutes.Text.Trim()); }
            set { maskedTextBoxTaskMinutes.Text = value.ToString(); }
        }

        public string TaskName
        {
            get { return labelConverterTaskName.Text; }
            set { labelConverterTaskName.Text = value; }
        }

        #endregion

        public event Action Save;
    }
}
