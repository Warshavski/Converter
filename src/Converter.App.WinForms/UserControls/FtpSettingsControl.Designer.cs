namespace Escyug.Converter.App.WinForms.UserControls
{
    partial class FtpSettingsControl
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.buttonSaveGuidesSettings = new System.Windows.Forms.Button();
            this.groupBoxFtpConn = new System.Windows.Forms.GroupBox();
            this.textBoxFtpUser = new System.Windows.Forms.TextBox();
            this.maskedTextBoxFtpPort = new System.Windows.Forms.MaskedTextBox();
            this.textBoxFtpPassword = new System.Windows.Forms.TextBox();
            this.labelFtpPassword = new System.Windows.Forms.Label();
            this.labelFtpUser = new System.Windows.Forms.Label();
            this.labelFtpPort = new System.Windows.Forms.Label();
            this.textBoxFtpHost = new System.Windows.Forms.TextBox();
            this.labelFtpHost = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBoxFtpConn.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.pictureBox4);
            this.panel2.Location = new System.Drawing.Point(450, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(57, 57);
            this.panel2.TabIndex = 28;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Image = global::Escyug.Converter.App.WinForms.Properties.Resources.esc_cube;
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(55, 55);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 22;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox4.Image = global::Escyug.Converter.App.WinForms.Properties.Resources._35__1_;
            this.pictureBox4.Location = new System.Drawing.Point(0, 0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(55, 55);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox4.TabIndex = 21;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Visible = false;
            // 
            // buttonSaveGuidesSettings
            // 
            this.buttonSaveGuidesSettings.Location = new System.Drawing.Point(328, 209);
            this.buttonSaveGuidesSettings.Name = "buttonSaveGuidesSettings";
            this.buttonSaveGuidesSettings.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveGuidesSettings.TabIndex = 26;
            this.buttonSaveGuidesSettings.Text = "Сохранить";
            this.buttonSaveGuidesSettings.UseVisualStyleBackColor = true;
            // 
            // groupBoxFtpConn
            // 
            this.groupBoxFtpConn.Controls.Add(this.textBoxFtpUser);
            this.groupBoxFtpConn.Controls.Add(this.maskedTextBoxFtpPort);
            this.groupBoxFtpConn.Controls.Add(this.textBoxFtpPassword);
            this.groupBoxFtpConn.Controls.Add(this.labelFtpPassword);
            this.groupBoxFtpConn.Controls.Add(this.labelFtpUser);
            this.groupBoxFtpConn.Controls.Add(this.labelFtpPort);
            this.groupBoxFtpConn.Controls.Add(this.textBoxFtpHost);
            this.groupBoxFtpConn.Controls.Add(this.labelFtpHost);
            this.groupBoxFtpConn.Location = new System.Drawing.Point(3, 3);
            this.groupBoxFtpConn.Name = "groupBoxFtpConn";
            this.groupBoxFtpConn.Size = new System.Drawing.Size(400, 200);
            this.groupBoxFtpConn.TabIndex = 25;
            this.groupBoxFtpConn.TabStop = false;
            this.groupBoxFtpConn.Text = "Подключение в FTP-серверу справочников :";
            // 
            // textBoxFtpUser
            // 
            this.textBoxFtpUser.Location = new System.Drawing.Point(76, 92);
            this.textBoxFtpUser.Name = "textBoxFtpUser";
            this.textBoxFtpUser.Size = new System.Drawing.Size(250, 20);
            this.textBoxFtpUser.TabIndex = 20;
            // 
            // maskedTextBoxFtpPort
            // 
            this.maskedTextBoxFtpPort.Location = new System.Drawing.Point(76, 61);
            this.maskedTextBoxFtpPort.Mask = "0000000";
            this.maskedTextBoxFtpPort.Name = "maskedTextBoxFtpPort";
            this.maskedTextBoxFtpPort.Size = new System.Drawing.Size(51, 20);
            this.maskedTextBoxFtpPort.TabIndex = 19;
            // 
            // textBoxFtpPassword
            // 
            this.textBoxFtpPassword.Location = new System.Drawing.Point(76, 123);
            this.textBoxFtpPassword.Name = "textBoxFtpPassword";
            this.textBoxFtpPassword.Size = new System.Drawing.Size(250, 20);
            this.textBoxFtpPassword.TabIndex = 18;
            this.textBoxFtpPassword.UseSystemPasswordChar = true;
            // 
            // labelFtpPassword
            // 
            this.labelFtpPassword.AutoSize = true;
            this.labelFtpPassword.Location = new System.Drawing.Point(8, 126);
            this.labelFtpPassword.Name = "labelFtpPassword";
            this.labelFtpPassword.Size = new System.Drawing.Size(62, 13);
            this.labelFtpPassword.TabIndex = 17;
            this.labelFtpPassword.Text = "Password : ";
            // 
            // labelFtpUser
            // 
            this.labelFtpUser.AutoSize = true;
            this.labelFtpUser.Location = new System.Drawing.Point(28, 95);
            this.labelFtpUser.Name = "labelFtpUser";
            this.labelFtpUser.Size = new System.Drawing.Size(38, 13);
            this.labelFtpUser.TabIndex = 16;
            this.labelFtpUser.Text = "User : ";
            // 
            // labelFtpPort
            // 
            this.labelFtpPort.AutoSize = true;
            this.labelFtpPort.Location = new System.Drawing.Point(35, 64);
            this.labelFtpPort.Name = "labelFtpPort";
            this.labelFtpPort.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelFtpPort.Size = new System.Drawing.Size(35, 13);
            this.labelFtpPort.TabIndex = 15;
            this.labelFtpPort.Text = "Port : ";
            // 
            // textBoxFtpHost
            // 
            this.textBoxFtpHost.Location = new System.Drawing.Point(76, 30);
            this.textBoxFtpHost.Name = "textBoxFtpHost";
            this.textBoxFtpHost.Size = new System.Drawing.Size(250, 20);
            this.textBoxFtpHost.TabIndex = 13;
            // 
            // labelFtpHost
            // 
            this.labelFtpHost.AutoSize = true;
            this.labelFtpHost.Location = new System.Drawing.Point(32, 33);
            this.labelFtpHost.Name = "labelFtpHost";
            this.labelFtpHost.Size = new System.Drawing.Size(38, 13);
            this.labelFtpHost.TabIndex = 12;
            this.labelFtpHost.Text = "Host : ";
            // 
            // FtpSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.buttonSaveGuidesSettings);
            this.Controls.Add(this.groupBoxFtpConn);
            this.Name = "FtpSettingsControl";
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBoxFtpConn.ResumeLayout(false);
            this.groupBoxFtpConn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button buttonSaveGuidesSettings;
        private System.Windows.Forms.GroupBox groupBoxFtpConn;
        private System.Windows.Forms.TextBox textBoxFtpUser;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxFtpPort;
        private System.Windows.Forms.TextBox textBoxFtpPassword;
        private System.Windows.Forms.Label labelFtpPassword;
        private System.Windows.Forms.Label labelFtpUser;
        private System.Windows.Forms.Label labelFtpPort;
        private System.Windows.Forms.TextBox textBoxFtpHost;
        private System.Windows.Forms.Label labelFtpHost;
    }
}
