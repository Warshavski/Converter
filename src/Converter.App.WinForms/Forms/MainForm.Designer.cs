namespace Escyug.Converter.App.WinForms.Forms
{
    partial class MainForm
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
            this.panelPictureBox = new System.Windows.Forms.Panel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.pictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.panelPictureBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // panelPictureBox
            // 
            this.panelPictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.panelPictureBox.Controls.Add(this.pictureBoxLogo);
            this.panelPictureBox.Controls.Add(this.pictureBoxLoading);
            this.panelPictureBox.Location = new System.Drawing.Point(75, 13);
            this.panelPictureBox.Name = "panelPictureBox";
            this.panelPictureBox.Size = new System.Drawing.Size(57, 57);
            this.panelPictureBox.TabIndex = 22;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLogo.Image = global::Escyug.Converter.App.WinForms.Properties.Resources.esc_cube;
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(57, 57);
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
            this.pictureBoxLoading.Size = new System.Drawing.Size(57, 57);
            this.pictureBoxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxLoading.TabIndex = 21;
            this.pictureBoxLoading.TabStop = false;
            this.pictureBoxLoading.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(206, 82);
            this.ControlBox = false;
            this.Controls.Add(this.panelPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panelPictureBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPictureBox;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.PictureBox pictureBoxLoading;
    }
}