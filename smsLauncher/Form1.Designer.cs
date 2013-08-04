namespace smsLauncher
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pbSignal = new System.Windows.Forms.PictureBox();
            this.lblNetwork = new System.Windows.Forms.Label();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.cbOutbound = new System.Windows.Forms.ComboBox();
            this.cbNetwork = new System.Windows.Forms.ComboBox();
            this.btnOn = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtTerminal = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.signalTimer = new System.Windows.Forms.Timer(this.components);
            this.processTimer = new System.Windows.Forms.Timer(this.components);
            this.lblNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbSignal)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSignal
            // 
            this.pbSignal.BackColor = System.Drawing.Color.Transparent;
            this.pbSignal.Image = global::smsLauncher.Properties.Resources.no_signal;
            this.pbSignal.Location = new System.Drawing.Point(9, 9);
            this.pbSignal.Name = "pbSignal";
            this.pbSignal.Size = new System.Drawing.Size(49, 9);
            this.pbSignal.TabIndex = 0;
            this.pbSignal.TabStop = false;
            // 
            // lblNetwork
            // 
            this.lblNetwork.AutoSize = true;
            this.lblNetwork.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetwork.Location = new System.Drawing.Point(57, 6);
            this.lblNetwork.Name = "lblNetwork";
            this.lblNetwork.Size = new System.Drawing.Size(41, 13);
            this.lblNetwork.TabIndex = 1;
            this.lblNetwork.Text = "Offline";
            // 
            // cbPort
            // 
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Location = new System.Drawing.Point(9, 24);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(79, 21);
            this.cbPort.TabIndex = 2;
            // 
            // cbType
            // 
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "TYPE",
            "SENDER ONLY",
            "RECEIVER ONLY",
            "ALL"});
            this.cbType.Location = new System.Drawing.Point(93, 24);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(121, 21);
            this.cbType.TabIndex = 3;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // cbOutbound
            // 
            this.cbOutbound.FormattingEnabled = true;
            this.cbOutbound.Items.AddRange(new object[] {
            "OUTBOUND",
            "1,2,3,4,5,6,7,8,9,0",
            "1,2,3,4,5",
            "6,7,8,9,0"});
            this.cbOutbound.Location = new System.Drawing.Point(9, 49);
            this.cbOutbound.Name = "cbOutbound";
            this.cbOutbound.Size = new System.Drawing.Size(105, 21);
            this.cbOutbound.TabIndex = 4;
            // 
            // cbNetwork
            // 
            this.cbNetwork.FormattingEnabled = true;
            this.cbNetwork.Items.AddRange(new object[] {
            "NETWORK",
            "SMART & TNT",
            "GLOBE & TM",
            "SUN CELLULAR",
            "SMART+GLOBE",
            "GLOBE+SUN",
            "SUN+SMART",
            "ALL NETWORK"});
            this.cbNetwork.Location = new System.Drawing.Point(118, 49);
            this.cbNetwork.Name = "cbNetwork";
            this.cbNetwork.Size = new System.Drawing.Size(96, 21);
            this.cbNetwork.TabIndex = 5;
            // 
            // btnOn
            // 
            this.btnOn.Location = new System.Drawing.Point(175, 76);
            this.btnOn.Name = "btnOn";
            this.btnOn.Size = new System.Drawing.Size(39, 24);
            this.btnOn.TabIndex = 6;
            this.btnOn.Text = "ON";
            this.btnOn.UseVisualStyleBackColor = true;
            this.btnOn.Click += new System.EventHandler(this.btnOn_Click);
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(118, 76);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(57, 24);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtTerminal
            // 
            this.txtTerminal.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtTerminal.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTerminal.Location = new System.Drawing.Point(-2, 106);
            this.txtTerminal.Multiline = true;
            this.txtTerminal.Name = "txtTerminal";
            this.txtTerminal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTerminal.Size = new System.Drawing.Size(225, 36);
            this.txtTerminal.TabIndex = 8;
            this.txtTerminal.Text = "Offline...";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(1, 128);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 13);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Status...";
            // 
            // signalTimer
            // 
            this.signalTimer.Interval = 1000;
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(6, 82);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(11, 13);
            this.lblNumber.TabIndex = 10;
            this.lblNumber.Text = "-";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(220, 142);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtTerminal);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnOn);
            this.Controls.Add(this.cbNetwork);
            this.Controls.Add(this.cbOutbound);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.cbPort);
            this.Controls.Add(this.pbSignal);
            this.Controls.Add(this.lblNetwork);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SMSLauncher";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSignal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSignal;
        private System.Windows.Forms.Label lblNetwork;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.ComboBox cbNetwork;
        private System.Windows.Forms.Button btnOn;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer signalTimer;
        private System.Windows.Forms.Timer processTimer;
        public System.Windows.Forms.ComboBox cbOutbound;
        public System.Windows.Forms.TextBox txtTerminal;
        private System.Windows.Forms.Label lblNumber;
    }
}

