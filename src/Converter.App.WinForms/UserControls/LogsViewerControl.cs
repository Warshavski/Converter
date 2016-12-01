using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Escyug.Converter.App.WinForms.Forms;
using Escyug.Converter.Presentation.Views;
using Escyug.Converter.Models.Entities;

namespace Escyug.Converter.App.WinForms.UserControls
{
    public partial class LogsViewerControl : SettingsControl, ILogsView
    {
        public LogsViewerControl(ApplicationContext context)
            : base(context)
        {
            InitializeComponent();
        }

        public new void Show()
        {
            (Context.MainForm as SettingsForm)?.tabPageLogs.Controls.Add(this);
        }

        public ICollection<LogRow> LogsList
        {
            set { dataGridViewLogs.DataSource = value; }
        }
    }
}
