namespace GTAPanicButton
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.labelMain = new System.Windows.Forms.Label();
            this.labelDesc1 = new System.Windows.Forms.Label();
            this.labelWarn = new System.Windows.Forms.Label();
            this.labelDesc2 = new System.Windows.Forms.Label();
            this.btnCredits = new System.Windows.Forms.Button();
            this.progressBarTimer = new System.Windows.Forms.ProgressBar();
            this.processSuspendWorker = new System.ComponentModel.BackgroundWorker();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.controllerWorker = new System.ComponentModel.BackgroundWorker();
            this.processDestroyWorker = new System.ComponentModel.BackgroundWorker();
            this.buttonOptions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelMain
            // 
            this.labelMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMain.Location = new System.Drawing.Point(12, 25);
            this.labelMain.Name = "labelMain";
            this.labelMain.Size = new System.Drawing.Size(654, 55);
            this.labelMain.TabIndex = 0;
            this.labelMain.Text = "GTA Panic Button";
            this.labelMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDesc1
            // 
            this.labelDesc1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDesc1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.labelDesc1.Location = new System.Drawing.Point(0, 123);
            this.labelDesc1.Name = "labelDesc1";
            this.labelDesc1.Size = new System.Drawing.Size(677, 26);
            this.labelDesc1.TabIndex = 1;
            this.labelDesc1.Text = "Press Ctrl + Shift + F11 (or L2 + R2 + L3) to close the game.";
            this.labelDesc1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelWarn
            // 
            this.labelWarn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWarn.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelWarn.Location = new System.Drawing.Point(0, 80);
            this.labelWarn.Name = "labelWarn";
            this.labelWarn.Size = new System.Drawing.Size(676, 32);
            this.labelWarn.TabIndex = 2;
            this.labelWarn.Text = "Don\'t close this window or the panic button won\'t work.";
            this.labelWarn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDesc2
            // 
            this.labelDesc2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDesc2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.labelDesc2.Location = new System.Drawing.Point(0, 151);
            this.labelDesc2.Name = "labelDesc2";
            this.labelDesc2.Size = new System.Drawing.Size(677, 26);
            this.labelDesc2.TabIndex = 3;
            this.labelDesc2.Text = "Press Ctrl + Shift + F12 (or L2 + R2 + R3) to suspend GTA.";
            this.labelDesc2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCredits
            // 
            this.btnCredits.Location = new System.Drawing.Point(576, 256);
            this.btnCredits.Name = "btnCredits";
            this.btnCredits.Size = new System.Drawing.Size(90, 35);
            this.btnCredits.TabIndex = 4;
            this.btnCredits.Text = "About";
            this.btnCredits.UseVisualStyleBackColor = true;
            this.btnCredits.Click += new System.EventHandler(this.BtnCredits_Click);
            // 
            // progressBarTimer
            // 
            this.progressBarTimer.Location = new System.Drawing.Point(12, 198);
            this.progressBarTimer.Name = "progressBarTimer";
            this.progressBarTimer.Size = new System.Drawing.Size(654, 50);
            this.progressBarTimer.TabIndex = 6;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "The GTA Panic Button has minimised to your task bar. Click on the icon in the tas" +
    "k bar to maximise it again.";
            this.notifyIcon.BalloonTipTitle = "I\'m down here!";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "GTA Panic Button";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseClick);
            // 
            // buttonOptions
            // 
            this.buttonOptions.Location = new System.Drawing.Point(12, 256);
            this.buttonOptions.Name = "buttonOptions";
            this.buttonOptions.Size = new System.Drawing.Size(90, 35);
            this.buttonOptions.TabIndex = 7;
            this.buttonOptions.Text = "Options";
            this.buttonOptions.UseVisualStyleBackColor = true;
            this.buttonOptions.Click += new System.EventHandler(this.ButtonOptions_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 299);
            this.Controls.Add(this.buttonOptions);
            this.Controls.Add(this.progressBarTimer);
            this.Controls.Add(this.btnCredits);
            this.Controls.Add(this.labelDesc2);
            this.Controls.Add(this.labelWarn);
            this.Controls.Add(this.labelDesc1);
            this.Controls.Add(this.labelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "GTA Panic Button";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelMain;
        private System.Windows.Forms.Label labelDesc1;
        private System.Windows.Forms.Label labelWarn;
        private System.Windows.Forms.Label labelDesc2;
        private System.Windows.Forms.Button btnCredits;
        private System.Windows.Forms.ProgressBar progressBarTimer;
        private System.ComponentModel.BackgroundWorker processSuspendWorker;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.ComponentModel.BackgroundWorker controllerWorker;
        private System.ComponentModel.BackgroundWorker processDestroyWorker;
        private System.Windows.Forms.Button buttonOptions;
    }
}

