using System;
using System.Threading.Tasks;
using System.Windows.Forms;

using Escyug.Converter.Presentation.Views;

namespace Escyug.Converter.App.WinForms.Forms
{
    public partial class MainForm : Form, IMainView
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly ApplicationContext _context;



        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------

        public MainForm(ApplicationContext context)
        {
            _context = context;

            InitializeComponent();

            this.Load += async (sender, e) => 
                await Invoker.InvokeAsync(InitializeAsync);
        }



        // PUBLIC INTERFACE MEMBERS SECTION
        //---------------------------------------------------------------------
        
        #region IView members

        public event Action InitializeView;

        public new void Show()
        {
            _context.MainForm = this;
            Application.Run(_context);
        }

        public string Error
        {
            set
            {
                MessageBox.Show(value, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string Notify
        {
            set
            {
                MessageBox.Show(value, "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public string Warning
        {
            set
            {
                MessageBox.Show(value, "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion IView members
        
        
        #region IMainView members

        public event Func<Task> InitializeAsync;

        public bool IsBusy
        {
            get
            {
                return pictureBoxLoading.Visible;
            }
            set
            {
                pictureBoxLogo.Visible = !value;
                pictureBoxLoading.Visible = value;
            }
        }

        #endregion
        
    }
}
