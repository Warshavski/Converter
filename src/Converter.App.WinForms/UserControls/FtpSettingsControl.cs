using System;
using System.Windows.Forms;

using Escyug.Converter.App.WinForms.Forms;
using Escyug.Converter.Presentation.Views;

namespace Escyug.Converter.App.WinForms.UserControls
{
    public partial class FtpSettingsControl : SettingsControl, IFtpSettingsView
    {
        public FtpSettingsControl(ApplicationContext context)
            :base(context)
        {
            InitializeComponent();

            buttonSaveGuidesSettings.Click += (sender, e) => Invoker.Invoke(Save);
        }

        public new void Show()
        {
            (Context.MainForm as SettingsForm)?.tabPageGuides.Controls.Add(this);
        }
        

        #region ISettingsView members

        public event Action Save;

        #endregion ISettingsView members


        #region IFtpSettingsView members

        public string FtpHost
        {
            get { return textBoxFtpHost.Text.Trim(); }
            set { textBoxFtpHost.Text = value; }
        }

        public int FtpPort
        {
            get { return int.Parse(maskedTextBoxFtpPort.Text.Trim()); }
            set { maskedTextBoxFtpPort.Text = value.ToString(); }
        }

        public string FtpUser
        {
            get { return textBoxFtpUser.Text.Trim(); }
            set { textBoxFtpUser.Text = value; }
        }

        public string FtpPassword
        {
            get { return textBoxFtpPassword.Text.Trim(); }
            set { textBoxFtpPassword.Text = value; }
        }

        #endregion IGuidesSettingsView

    }
}
