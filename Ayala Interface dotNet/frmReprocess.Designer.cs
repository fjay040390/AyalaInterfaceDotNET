namespace Ayala_Interface_dotNet
{
    partial class frmReprocess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReprocess));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDateStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.monthEnd = new System.Windows.Forms.MonthCalendar();
            this.label1 = new System.Windows.Forms.Label();
            this.monthStart = new System.Windows.Forms.MonthCalendar();
            this.btnReprocess = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDateStatus);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pbProgress);
            this.groupBox1.Controls.Add(this.lblEnd);
            this.groupBox1.Controls.Add(this.lblStart);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.monthEnd);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.monthStart);
            this.groupBox1.Controls.Add(this.btnReprocess);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 309);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblDateStatus
            // 
            this.lblDateStatus.AutoSize = true;
            this.lblDateStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblDateStatus.Location = new System.Drawing.Point(74, 209);
            this.lblDateStatus.Name = "lblDateStatus";
            this.lblDateStatus.Size = new System.Drawing.Size(65, 13);
            this.lblDateStatus.TabIndex = 10;
            this.lblDateStatus.Text = "00/00/0000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Generating:";
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(9, 227);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(472, 23);
            this.pbProgress.TabIndex = 8;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(412, 16);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(65, 13);
            this.lblEnd.TabIndex = 7;
            this.lblEnd.Text = "00/00/0000";
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(167, 16);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(65, 13);
            this.lblStart.TabIndex = 6;
            this.lblStart.Text = "00/00/0000";
            this.lblStart.Click += new System.EventHandler(this.lblStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "End Session";
            // 
            // monthEnd
            // 
            this.monthEnd.Location = new System.Drawing.Point(254, 38);
            this.monthEnd.Name = "monthEnd";
            this.monthEnd.TabIndex = 4;
            this.monthEnd.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthEnd_DateChanged);
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
            this.monthStart.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthStart_DateChanged);
            // 
            // btnReprocess
            // 
            this.btnReprocess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnReprocess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aqua;
            this.btnReprocess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReprocess.Location = new System.Drawing.Point(343, 256);
            this.btnReprocess.Name = "btnReprocess";
            this.btnReprocess.Size = new System.Drawing.Size(139, 47);
            this.btnReprocess.TabIndex = 1;
            this.btnReprocess.Text = "Reprocess";
            this.btnReprocess.UseVisualStyleBackColor = true;
            this.btnReprocess.Click += new System.EventHandler(this.btnReprocess_Click);
            // 
            // frmReprocess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 333);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReprocess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reprocess File";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnReprocess;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MonthCalendar monthEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MonthCalendar monthStart;
        private System.Windows.Forms.Label lblDateStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar pbProgress;
    }
}