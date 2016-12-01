using System;
using System.Windows.Forms;

using Escyug.Converter.Presentation.Common;


namespace Escyug.Converter.App.WinForms.UserControls
{
    public partial class SettingsControl : UserControl, IView
    {
        protected ApplicationContext Context { get; private set; }

        public SettingsControl()
        {
            InitializeComponent();

            Load += (sender, e) => Invoker.Invoke(InitializeView);
        }

        protected SettingsControl(ApplicationContext context)
            : this()
        {
            Context = context;
        }

        #region IView members

        public string Error
        {
            set
            {
                MessageBox.Show(value, "Application error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string Notify
        {
            set
            {
                MessageBox.Show(value, "Application notification",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public string Warning
        {
            set
            {
                MessageBox.Show(value, "Application warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public event Action InitializeView;

        public void Close()
        {
            throw new NotImplementedException();
        }

        #endregion IView members
    }
}
