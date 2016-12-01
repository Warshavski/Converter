namespace Escyug.Converter.App.WinForms.Forms
{
    partial class SettingsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonClose = new System.Windows.Forms.Button();
            this.tabPageConnections = new System.Windows.Forms.TabPage();
            this.tabPageLogs = new System.Windows.Forms.TabPage();
            this.tabControlConfigurations = new System.Windows.Forms.TabControl();
            this.tabPageGuides = new System.Windows.Forms.TabPage();
            this.tabPageSender = new System.Windows.Forms.TabPage();
            this.tabPageTask = new System.Windows.Forms.TabPage();
            this.tabControlConfigurations.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(462, 346);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // tabPageConnections
            // 
            this.tabPageConnections.Location = new System.Drawing.Point(4, 22);
            this.tabPageConnections.Name = "tabPageConnections";
            this.tabPageConnections.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConnections.Size = new System.Drawing.Size(524, 310);
            this.tabPageConnections.TabIndex = 1;
            this.tabPageConnections.Text = "Подключения";
            this.tabPageConnections.UseVisualStyleBackColor = true;
            // 
            // tabPageLogs
            // 
            this.tabPageLogs.Location = new System.Drawing.Point(4, 22);
            this.tabPageLogs.Name = "tabPageLogs";
            this.tabPageLogs.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLogs.Size = new System.Drawing.Size(524, 310);
            this.tabPageLogs.TabIndex = 0;
            this.tabPageLogs.Text = "Логи";
            this.tabPageLogs.UseVisualStyleBackColor = true;
            // 
            // tabControlConfigurations
            // 
            this.tabControlConfigurations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlConfigurations.Controls.Add(this.tabPageLogs);
            this.tabControlConfigurations.Controls.Add(this.tabPageConnections);
            this.tabControlConfigurations.Controls.Add(this.tabPageGuides);
            this.tabControlConfigurations.Controls.Add(this.tabPageSender);
            this.tabControlConfigurations.Controls.Add(this.tabPageTask);
            this.tabControlConfigurations.Location = new System.Drawing.Point(5, 4);
            this.tabControlConfigurations.Name = "tabControlConfigurations";
            this.tabControlConfigurations.SelectedIndex = 0;
            this.tabControlConfigurations.Size = new System.Drawing.Size(532, 336);
            this.tabControlConfigurations.TabIndex = 3;
            // 
            // tabPageGuides
            // 
            this.tabPageGuides.Location = new System.Drawing.Point(4, 22);
            this.tabPageGuides.Name = "tabPageGuides";
            this.tabPageGuides.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGuides.Size = new System.Drawing.Size(524, 310);
            this.tabPageGuides.TabIndex = 2;
            this.tabPageGuides.Text = "Справочники";
            this.tabPageGuides.UseVisualStyleBackColor = true;
            // 
            // tabPageSender
            // 
            this.tabPageSender.Location = new System.Drawing.Point(4, 22);
            this.tabPageSender.Name = "tabPageSender";
            this.tabPageSender.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSender.Size = new System.Drawing.Size(524, 310);
            this.tabPageSender.TabIndex = 3;
            this.tabPageSender.Text = "Почта";
            this.tabPageSender.UseVisualStyleBackColor = true;
            // 
            // tabPageTask
            // 
            this.tabPageTask.Location = new System.Drawing.Point(4, 22);
            this.tabPageTask.Name = "tabPageTask";
            this.tabPageTask.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTask.Size = new System.Drawing.Size(524, 310);
            this.tabPageTask.TabIndex = 4;
            this.tabPageTask.Text = "Автозапуск";
            this.tabPageTask.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 373);
            this.Controls.Add(this.tabControlConfigurations);
            this.Controls.Add(this.buttonClose);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.tabControlConfigurations.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TabControl tabControlConfigurations;
        public System.Windows.Forms.TabPage tabPageLogs;
        public System.Windows.Forms.TabPage tabPageConnections;
        public System.Windows.Forms.TabPage tabPageGuides;
        public System.Windows.Forms.TabPage tabPageSender;
        public System.Windows.Forms.TabPage tabPageTask;
    }
}