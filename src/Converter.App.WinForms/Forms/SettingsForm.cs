using System;
using System.Threading.Tasks;
using System.Windows.Forms;

using Escyug.Converter.Presentation.Views;


namespace Escyug.Converter.App.WinForms.Forms
{
    public partial class SettingsForm : Form, ISettingsCompositeView
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly ApplicationContext _context;



        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------

        public SettingsForm(ApplicationContext context)
        {
            _context = context;

            InitializeComponent();


            // EVENTS BINDINGS SECTION
            //---------------------------------------------
            Load += (sender, e) => Invoker.Invoke(InitializeView);
            //Load += async (sender, e) => await Invoker.InvokeAsync(InitializeViewAsync);

            buttonClose.Click += (sender, e) => { Close(); };
        }



        // INTERFACE MEMBERS SECTION
        //---------------------------------------------------------------------

        #region IView members

        public new void Show()
        {
            _context.MainForm = this;
            Application.Run(_context);
        }

        public string Error
        {
            set
            {
                MessageBox.Show(value, @"Application error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string Notify
        {
            set
            {
                MessageBox.Show(value, @"Application notification",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public string Warning
        {
            set
            {
                MessageBox.Show(value, @"Application warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        public event Action InitializeView;

        #endregion IView members

        public event Func<Task> InitializeViewAsync;

    }
}