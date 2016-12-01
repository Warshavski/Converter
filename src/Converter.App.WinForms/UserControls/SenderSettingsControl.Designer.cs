namespace Escyug.Converter.App.WinForms.UserControls
{
    partial class SenderSettingsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelPictureBox = new System.Windows.Forms.Panel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.pictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.groupBoxSenderConfiguration = new System.Windows.Forms.GroupBox();
            this.textBoxSenderReceiver = new System.Windows.Forms.TextBox();
            this.labelSenderReceiver = new System.Windows.Forms.Label();
            this.textBoxSenderLogin = new System.Windows.Forms.TextBox();
            this.maskedTextBoxSenderPort = new System.Windows.Forms.MaskedTextBox();
            this.textBoxSenderPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxSenderHost = new System.Windows.Forms.TextBox();
            this.labelHost = new System.Windows.Forms.Label();
            this.buttonSaveSenderSettings = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelPictureBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).BeginInit();
            this.groupBoxSenderConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelPictureBox
            // 
            this.panelPictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.panelPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPictureBox.Controls.Add(this.pictureBoxLogo);
            this.panelPictureBox.Controls.Add(this.pictureBoxLoading);
            this.panelPictureBox.Location = new System.Drawing.Point(450, 12);
            this.panelPictureBox.Name = "panelPictureBox";
            this.panelPictureBox.Size = new System.Drawing.Size(57, 57);
            this.panelPictureBox.TabIndex = 26;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLogo.Image = global::Escyug.Converter.App.WinForms.Properties.Resources.esc_cube;
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(55, 55);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 22;
            this.pictureBoxLogo.TabStop = false;
            // 
            // pictureBoxLoading
            // 
            this.pictureBoxLoading.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLoading.Image = global::Escyug.Converter.App.WinForms.Properties.Resources._35__1_;
            this.pictureBoxLoading.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLoading.Name = "pictureBoxLoading";
            this.pictureBoxLoading.Size = new System.Drawing.Size(55, 55);
            this.pictureBoxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxLoading.TabIndex = 21;
            this.pictureBoxLoading.TabStop = false;
            this.pictureBoxLoading.Visible = false;
            // 
            // groupBoxSenderConfiguration
            // 
            this.groupBoxSenderConfiguration.Controls.Add(this.textBoxSenderReceiver);
            this.groupBoxSenderConfiguration.Controls.Add(this.labelSenderReceiver);
            this.groupBoxSenderConfiguration.Controls.Add(this.textBoxSenderLogin);
            this.groupBoxSenderConfiguration.Controls.Add(this.maskedTextBoxSenderPort);
            this.groupBoxSenderConfiguration.Controls.Add(this.textBoxSenderPassword);
            this.groupBoxSenderConfiguration.Controls.Add(this.labelPassword);
            this.groupBoxSenderConfiguration.Controls.Add(this.labelLogin);
            this.groupBoxSenderConfiguration.Controls.Add(this.labelPort);
            this.groupBoxSenderConfiguration.Controls.Add(this.textBoxSenderHost);
            this.groupBoxSenderConfiguration.Controls.Add(this.labelHost);
            this.groupBoxSenderConfiguration.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSenderConfiguration.Name = "groupBoxSenderConfiguration";
            this.groupBoxSenderConfiguration.Size = new System.Drawing.Size(400, 200);
            this.groupBoxSenderConfiguration.TabIndex = 25;
            this.groupBoxSenderConfiguration.TabStop = false;
            this.groupBoxSenderConfiguration.Text = "Настройки SMTP-клиента :";
            // 
            // textBoxSenderReceiver
            // 
            this.textBoxSenderReceiver.Location = new System.Drawing.Point(76, 154);
            this.textBoxSenderReceiver.Name = "textBoxSenderReceiver";
            this.textBoxSenderReceiver.Size = new System.Drawing.Size(250, 20);
            this.textBoxSenderReceiver.TabIndex = 23;
            // 
            // labelSenderReceiver
            // 
            this.labelSenderReceiver.AutoSize = true;
            this.labelSenderReceiver.Location = new System.Drawing.Point(14, 157);
            this.labelSenderReceiver.Name = "labelSenderReceiver";
            this.labelSenderReceiver.Size = new System.Drawing.Size(56, 13);
            this.labelSenderReceiver.TabIndex = 22;
            this.labelSenderReceiver.Text = "Receiver :";
            // 
            // textBoxSenderLogin
            // 
            this.textBoxSenderLogin.Location = new System.Drawing.Point(76, 92);
            this.textBoxSenderLogin.Name = "textBoxSenderLogin";
            this.textBoxSenderLogin.Size = new System.Drawing.Size(250, 20);
            this.textBoxSenderLogin.TabIndex = 11;
            // 
            // maskedTextBoxSenderPort
            // 
            this.maskedTextBoxSenderPort.Location = new System.Drawing.Point(76, 61);
            this.maskedTextBoxSenderPort.Mask = "0000000";
            this.maskedTextBoxSenderPort.Name = "maskedTextBoxSenderPort";
            this.maskedTextBoxSenderPort.Size = new System.Drawing.Size(51, 20);
            this.maskedTextBoxSenderPort.TabIndex = 10;
            // 
            // textBoxSenderPassword
            // 
            this.textBoxSenderPassword.Location = new System.Drawing.Point(76, 123);
            this.textBoxSenderPassword.Name = "textBoxSenderPassword";
            this.textBoxSenderPassword.Size = new System.Drawing.Size(250, 20);
            this.textBoxSenderPassword.TabIndex = 9;
            this.textBoxSenderPassword.UseSystemPasswordChar = true;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(8, 126);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(62, 13);
            this.labelPassword.TabIndex = 8;
            this.labelPassword.Text = "Password : ";
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(28, 95);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(42, 13);
            this.labelLogin.TabIndex = 6;
            this.labelLogin.Text = "Login : ";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(35, 64);
            this.labelPort.Name = "labelPort";
            this.labelPort.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelPort.Size = new System.Drawing.Size(35, 13);
            this.labelPort.TabIndex = 4;
            this.labelPort.Text = "Port : ";
            // 
            // textBoxSenderHost
            // 
            this.textBoxSenderHost.Location = new System.Drawing.Point(76, 30);
            this.textBoxSenderHost.Name = "textBoxSenderHost";
            this.textBoxSenderHost.Size = new System.Drawing.Size(250, 20);
            this.textBoxSenderHost.TabIndex = 1;
            // 
            // labelHost
            // 
            this.labelHost.AutoSize = true;
            this.labelHost.Location = new System.Drawing.Point(32, 33);
            this.labelHost.Name = "labelHost";
            this.labelHost.Size = new System.Drawing.Size(38, 13);
            this.labelHost.TabIndex = 0;
            this.labelHost.Text = "Host : ";
            // 
            // buttonSaveSenderSettings
            // 
            this.buttonSaveSenderSettings.Location = new System.Drawing.Point(328, 209);
            this.buttonSaveSenderSettings.Name = "buttonSaveSenderSettings";
            this.buttonSaveSenderSettings.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveSenderSettings.TabIndex = 23;
            this.buttonSaveSenderSettings.Text = "Сохранить";
            this.buttonSaveSenderSettings.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // SenderSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelPictureBox);
            this.Controls.Add(this.groupBoxSenderConfiguration);
            this.Controls.Add(this.buttonSaveSenderSettings);
            this.Name = "SenderSettingsControl";
            this.panelPictureBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).EndInit();
            this.groupBoxSenderConfiguration.ResumeLayout(false);
            this.groupBoxSenderConfiguration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPictureBox;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.PictureBox pictureBoxLoading;
        private System.Windows.Forms.GroupBox groupBoxSenderConfiguration;
        private System.Windows.Forms.TextBox textBoxSenderReceiver;
        private System.Windows.Forms.Label labelSenderReceiver;
        private System.Windows.Forms.TextBox textBoxSenderLogin;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxSenderPort;
        private System.Windows.Forms.TextBox textBoxSenderPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxSenderHost;
        private System.Windows.Forms.Label labelHost;
        private System.Windows.Forms.Button buttonSaveSenderSettings;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
