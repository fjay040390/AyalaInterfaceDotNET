namespace Ayala_Interface_dotNet
{
    partial class mdiMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mdiMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolConfiguration = new System.Windows.Forms.ToolStripButton();
            this.toolSession = new System.Windows.Forms.ToolStripButton();
            this.toolReprocess = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolExit,
            this.toolStripSeparator1,
            this.toolConfiguration,
            this.toolSession,
            this.toolReprocess});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(739, 42);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolExit
            // 
            this.toolExit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolExit.Image = ((System.Drawing.Image)(resources.GetObject("toolExit.Image")));
            this.toolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(34, 39);
            this.toolExit.Text = "E&xit";
            this.toolExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 42);
            // 
            // toolConfiguration
            // 
            this.toolConfiguration.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolConfiguration.Image = ((System.Drawing.Image)(resources.GetObject("toolConfiguration.Image")));
            this.toolConfiguration.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolConfiguration.Name = "toolConfiguration";
            this.toolConfiguration.Size = new System.Drawing.Size(97, 39);
            this.toolConfiguration.Text = "Configuration";
            this.toolConfiguration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolConfiguration.Click += new System.EventHandler(this.toolConfiguration_Click);
            // 
            // toolSession
            // 
            this.toolSession.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolSession.Image = ((System.Drawing.Image)(resources.GetObject("toolSession.Image")));
            this.toolSession.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSession.Name = "toolSession";
            this.toolSession.Size = new System.Drawing.Size(58, 39);
            this.toolSession.Text = "Session";
            this.toolSession.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSession.Click += new System.EventHandler(this.toolSession_Click);
            // 
            // toolReprocess
            // 
            this.toolReprocess.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolReprocess.Image = ((System.Drawing.Image)(resources.GetObject("toolReprocess.Image")));
            this.toolReprocess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolReprocess.Name = "toolReprocess";
            this.toolReprocess.Size = new System.Drawing.Size(74, 39);
            this.toolReprocess.Text = "Reprocess";
            this.toolReprocess.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolReprocess.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 434);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(739, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(246, 17);
            this.toolStripStatusLabel1.Text = "Developed by: The Softwarehaus Corporation";
            // 
            // mdiMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 456);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "mdiMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayala Interface dotNet Version 1.0";
            this.Load += new System.EventHandler(this.mdiMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.ToolStripButton toolConfiguration;
        private System.Windows.Forms.ToolStripButton toolSession;
        private System.Windows.Forms.ToolStripButton toolReprocess;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}



