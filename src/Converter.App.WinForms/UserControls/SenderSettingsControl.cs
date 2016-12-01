using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Escyug.Converter.Presentation.Views;
using Escyug.Converter.App.WinForms.Forms;
using System.Text.RegularExpressions;

namespace Escyug.Converter.App.WinForms.UserControls
{
    public partial class SenderSettingsControl : SettingsControl, ISenderSettingsView
    {
        public SenderSettingsControl(ApplicationContext context)
            : base(context)
        {
            InitializeComponent();

            textBoxSenderLogin.LostFocus += (sender, e) => CheckEmailAddress(textBoxSenderLogin);
            textBoxSenderLogin.LostFocus += (sender, e) => CheckEmailAddress(textBoxSenderReceiver);

            buttonSaveSenderSettings.Click += (sender, e) =>
            {
                if (CheckEmailAddress(textBoxSenderLogin) &&
                    CheckEmailAddress(textBoxSenderReceiver))
                {
                    Invoker.Invoke(Save);
                }
            };
        }


        public new void Show()
        {
            (Context.MainForm as SettingsForm)?.tabPageSender.Controls.Add(this);
        }

        #region ISenderSettingsView members

        public string Host
        {
            get { return textBoxSenderHost.Text.Trim(); }
            set { textBoxSenderHost.Text = value; }
        }

        public int Port
        {
            get { return int.Parse(maskedTextBoxSenderPort.Text.Trim()); }
            set { maskedTextBoxSenderPort.Text = value.ToString(); }
        }

        public string Password
        {
            get { return textBoxSenderPassword.Text.Trim(); }
            set { textBoxSenderPassword.Text = value; }
        }

        public string Login
        {
            get { return textBoxSenderLogin.Text.Trim(); }
            set { textBoxSenderLogin.Text = value; }
        }

        public string Receiver
        {
            get { return textBoxSenderReceiver.Text.Trim(); }
            set { textBoxSenderReceiver.Text = value; }
        }

        #endregion ISenderSettingsView

        public event Action Save;



        private bool CheckEmailAddress(TextBox emailTextBox)
        {
            bool isValidEmailAddress = false;

            Regex reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!reg.IsMatch(emailTextBox.Text.Trim()))
            {
                errorProvider1.SetError(emailTextBox, "Email not valid");
            }
            else
            {
                isValidEmailAddress = true;
                errorProvider1.Dispose();
            }

            return isValidEmailAddress;
        }
    }
}
