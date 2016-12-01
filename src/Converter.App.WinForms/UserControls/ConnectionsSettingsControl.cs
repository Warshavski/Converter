using System;
using System.Windows.Forms;

using Escyug.Converter.App.WinForms.Forms;
using Escyug.Converter.Presentation.Views;

namespace Escyug.Converter.App.WinForms.UserControls
{
    public partial class ConnectionsSettingsControl : SettingsControl, IConnectionsSettingsView
    {
        public ConnectionsSettingsControl(ApplicationContext context)
            :base(context)
        {
            InitializeComponent();

            buttonRecipesBrowse.Click += (sender, e) =>
            {
                var folderPath = GetFolderPath();
                if (!string.IsNullOrEmpty(folderPath))
                {
                    textBoxRecipesPath.Text = folderPath;
                }
            };

            buttonRemainsBrowse.Click += (sender, e) =>
            {
                var folderPath = GetFolderPath();
                if (!string.IsNullOrEmpty(folderPath))
                {
                    textBoxRamainsPath.Text = folderPath;
                }
            };

            buttonSaveConnSettings.Click += (sender, e) => Invoker.Invoke(Save);
        }

        #region ISettingsView members

        public event Action Save;

        #endregion ISettingsView members


        #region IView members override

        public new void Show()
        {
            (Context.MainForm as SettingsForm)?.tabPageConnections.Controls.Add(this);
        }

        #endregion IView members override

        #region IConnectionsSettingsView members

        public string RecipesFolderPath
        {
            get { return textBoxRecipesPath.Text.Trim(); }
            set { textBoxRecipesPath.Text = value; }
        }

        public string RecipesServiceAddress
        {
            get { return textBoxRecipesServiceConn.Text.Trim(); }
            set { textBoxRecipesServiceConn.Text = value; }
        }

        public string RemainsFolderPath
        {
            get { return textBoxRamainsPath.Text.Trim(); }
            set { textBoxRamainsPath.Text = value; }
        }
        
        public string RemainsServiceAddress
        {
            get { return textBoxRemainsServiceConn.Text.Trim(); }
            set { textBoxRemainsServiceConn.Text = value; }
        }

        #endregion IConnectionsSettingsView members


        private string GetFolderPath()
        {
            var folderPath = string.Empty;
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    folderPath = folderDialog.SelectedPath;
                }
            }

            return folderPath;
        }
    }
}
