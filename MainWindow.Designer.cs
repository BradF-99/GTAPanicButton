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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.labelMain = new System.Windows.Forms.Label();
            this.labelDesc1 = new System.Windows.Forms.Label();
            this.labelWarn = new System.Windows.Forms.Label();
            this.labelDesc2 = new System.Windows.Forms.Label();
            this.btnCredits = new System.Windows.Forms.Button();
            this.checkboxBeep = new System.Windows.Forms.CheckBox();
            this.progressBarTimer = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
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
            this.labelDesc1.Location = new System.Drawing.Point(0, 93);
            this.labelDesc1.Name = "labelDesc1";
            this.labelDesc1.Size = new System.Drawing.Size(677, 26);
            this.labelDesc1.TabIndex = 1;
            this.labelDesc1.Text = "Press Ctrl + Shift + F11 to kill the GTA and Social Club processes.";
            this.labelDesc1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelWarn
            // 
            this.labelWarn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWarn.Location = new System.Drawing.Point(1, 160);
            this.labelWarn.Name = "labelWarn";
            this.labelWarn.Size = new System.Drawing.Size(676, 32);
            this.labelWarn.TabIndex = 2;
            this.labelWarn.Text = "DO NOT CLOSE THIS WINDOW! Just minimise it.";
            this.labelWarn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDesc2
            // 
            this.labelDesc2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDesc2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.labelDesc2.Location = new System.Drawing.Point(12, 119);
            this.labelDesc2.Name = "labelDesc2";
            this.labelDesc2.Size = new System.Drawing.Size(654, 26);
            this.labelDesc2.TabIndex = 3;
            this.labelDesc2.Text = "Press Ctrl + Shift + F12 to suspend GTA for 10 seconds.";
            this.labelDesc2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCredits
            // 
            this.btnCredits.Location = new System.Drawing.Point(591, 254);
            this.btnCredits.Name = "btnCredits";
            this.btnCredits.Size = new System.Drawing.Size(75, 35);
            this.btnCredits.TabIndex = 4;
            this.btnCredits.Text = "About";
            this.btnCredits.UseVisualStyleBackColor = true;
            this.btnCredits.Click += new System.EventHandler(this.BtnCredits_Click);
            // 
            // checkboxBeep
            // 
            this.checkboxBeep.AutoSize = true;
            this.checkboxBeep.Location = new System.Drawing.Point(12, 260);
            this.checkboxBeep.Name = "checkboxBeep";
            this.checkboxBeep.Size = new System.Drawing.Size(160, 24);
            this.checkboxBeep.TabIndex = 5;
            this.checkboxBeep.Text = "Audio Cues (TTS)";
            this.checkboxBeep.UseVisualStyleBackColor = true;
            this.checkboxBeep.CheckedChanged += new System.EventHandler(this.CheckboxBeep_CheckedChanged);
            // 
            // progressBarTimer
            // 
            this.progressBarTimer.Location = new System.Drawing.Point(12, 208);
            this.progressBarTimer.Name = "progressBarTimer";
            this.progressBarTimer.Size = new System.Drawing.Size(654, 37);
            this.progressBarTimer.TabIndex = 6;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 299);
            this.Controls.Add(this.progressBarTimer);
            this.Controls.Add(this.checkboxBeep);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMain;
        private System.Windows.Forms.Label labelDesc1;
        private System.Windows.Forms.Label labelWarn;
        private System.Windows.Forms.Label labelDesc2;
        private System.Windows.Forms.Button btnCredits;
        private System.Windows.Forms.CheckBox checkboxBeep;
        private System.Windows.Forms.ProgressBar progressBarTimer;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}

