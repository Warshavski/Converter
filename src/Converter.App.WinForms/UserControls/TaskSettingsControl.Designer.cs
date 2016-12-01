namespace Escyug.Converter.App.WinForms.UserControls
{
    partial class TaskSettingsControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.buttonSaveTaskSettings = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.maskedTextBoxTaskMinutes = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBoxTaskHours = new System.Windows.Forms.MaskedTextBox();
            this.labelConverterTaskName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Location = new System.Drawing.Point(450, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(57, 57);
            this.panel1.TabIndex = 26;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Escyug.Converter.App.WinForms.Properties.Resources.esc_cube;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 55);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Image = global::Escyug.Converter.App.WinForms.Properties.Resources._35__1_;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(55, 55);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // buttonSaveTaskSettings
            // 
            this.buttonSaveTaskSettings.Location = new System.Drawing.Point(173, 145);
            this.buttonSaveTaskSettings.Name = "buttonSaveTaskSettings";
            this.buttonSaveTaskSettings.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveTaskSettings.TabIndex = 25;
            this.buttonSaveTaskSettings.Text = "Сохранить";
            this.buttonSaveTaskSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.maskedTextBoxTaskMinutes);
            this.groupBox5.Controls.Add(this.maskedTextBoxTaskHours);
            this.groupBox5.Controls.Add(this.labelConverterTaskName);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(245, 136);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Автозапуск :";
            // 
            // maskedTextBoxTaskMinutes
            // 
            this.maskedTextBoxTaskMinutes.Location = new System.Drawing.Point(77, 93);
            this.maskedTextBoxTaskMinutes.Mask = "00000";
            this.maskedTextBoxTaskMinutes.Name = "maskedTextBoxTaskMinutes";
            this.maskedTextBoxTaskMinutes.Size = new System.Drawing.Size(42, 20);
            this.maskedTextBoxTaskMinutes.TabIndex = 12;
            this.maskedTextBoxTaskMinutes.ValidatingType = typeof(int);
            // 
            // maskedTextBoxTaskHours
            // 
            this.maskedTextBoxTaskHours.Location = new System.Drawing.Point(77, 64);
            this.maskedTextBoxTaskHours.Mask = "00000";
            this.maskedTextBoxTaskHours.Name = "maskedTextBoxTaskHours";
            this.maskedTextBoxTaskHours.Size = new System.Drawing.Size(42, 20);
            this.maskedTextBoxTaskHours.TabIndex = 11;
            this.maskedTextBoxTaskHours.ValidatingType = typeof(int);
            // 
            // labelConverterTaskName
            // 
            this.labelConverterTaskName.AutoSize = true;
            this.labelConverterTaskName.Location = new System.Drawing.Point(125, 25);
            this.labelConverterTaskName.Name = "labelConverterTaskName";
            this.labelConverterTaskName.Size = new System.Drawing.Size(98, 13);
            this.labelConverterTaskName.TabIndex = 10;
            this.labelConverterTaskName.Text = "Converter Autostart";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Минуты :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Часы :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Название задачи :";
            // 
            // TaskSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonSaveTaskSettings);
            this.Controls.Add(this.groupBox5);
            this.Name = "TaskSettingsControl";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button buttonSaveTaskSettings;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxTaskMinutes;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxTaskHours;
        private System.Windows.Forms.Label labelConverterTaskName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
    }
}
