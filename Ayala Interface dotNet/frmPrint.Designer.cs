namespace Ayala_Interface_dotNet
{
    partial class frmPrint
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblStart = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.monthStart = new System.Windows.Forms.MonthCalendar();
            this.btnReprocess = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblStart);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.monthStart);
            this.groupBox1.Controls.Add(this.btnReprocess);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 269);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(167, 16);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(65, 13);
            this.lblStart.TabIndex = 6;
            this.lblStart.Text = "00/00/0000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Start Session";
            // 
            // monthStart
            // 
            this.monthStart.Location = new System.Drawing.Point(9, 38);
            this.monthStart.Name = "monthStart";
            this.monthStart.TabIndex = 2;
            // 
            // btnReprocess
            // 
            this.btnReprocess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnReprocess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aqua;
            this.btnReprocess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReprocess.Location = new System.Drawing.Point(97, 212);
            this.btnReprocess.Name = "btnReprocess";
            this.btnReprocess.Size = new System.Drawing.Size(139, 47);
            this.btnReprocess.TabIndex = 1;
            this.btnReprocess.Text = "Reprocess";
            this.btnReprocess.UseVisualStyleBackColor = true;
            // 
            // frmPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 289);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Report";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MonthCalendar monthStart;
        private System.Windows.Forms.Button btnReprocess;
    }
}