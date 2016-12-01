namespace Escyug.Converter.App.WinForms.UserControls
{
    partial class LogsViewerControl
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
            this.groupBoxLogs = new System.Windows.Forms.GroupBox();
            this.dataGridViewLogs = new System.Windows.Forms.DataGridView();
            this.DateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxLogs
            // 
            this.groupBoxLogs.Controls.Add(this.dataGridViewLogs);
            this.groupBoxLogs.Location = new System.Drawing.Point(6, 3);
            this.groupBoxLogs.Name = "groupBoxLogs";
            this.groupBoxLogs.Size = new System.Drawing.Size(511, 298);
            this.groupBoxLogs.TabIndex = 10;
            this.groupBoxLogs.TabStop = false;
            this.groupBoxLogs.Text = "Логи :";
            // 
            // dataGridViewLogs
            // 
            this.dataGridViewLogs.AllowUserToAddRows = false;
            this.dataGridViewLogs.AllowUserToDeleteRows = false;
            this.dataGridViewLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewLogs.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DateColumn,
            this.MessageColumn});
            this.dataGridViewLogs.Location = new System.Drawing.Point(6, 20);
            this.dataGridViewLogs.Name = "dataGridViewLogs";
            this.dataGridViewLogs.ReadOnly = true;
            this.dataGridViewLogs.Size = new System.Drawing.Size(499, 272);
            this.dataGridViewLogs.TabIndex = 0;
            // 
            // DateColumn
            // 
            this.DateColumn.DataPropertyName = "Date";
            this.DateColumn.HeaderText = "Дата";
            this.DateColumn.Name = "DateColumn";
            this.DateColumn.ReadOnly = true;
            this.DateColumn.Width = 120;
            // 
            // MessageColumn
            // 
            this.MessageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MessageColumn.DataPropertyName = "Message";
            this.MessageColumn.HeaderText = "Сообщение";
            this.MessageColumn.Name = "MessageColumn";
            this.MessageColumn.ReadOnly = true;
            // 
            // LogsViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxLogs);
            this.Name = "LogsViewerControl";
            this.Size = new System.Drawing.Size(523, 303);
            this.groupBoxLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLogs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLogs;
        private System.Windows.Forms.DataGridView dataGridViewLogs;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MessageColumn;
    }
}
