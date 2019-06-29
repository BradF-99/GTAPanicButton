namespace GTAPanicButton
{
    partial class SettingsWindow
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
            this.labelSettingsTitle = new System.Windows.Forms.Label();
            this.groupBoxKeyBinds = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBoxStartup = new System.Windows.Forms.CheckBox();
            this.checkBoxController = new System.Windows.Forms.CheckBox();
            this.labelAudioCues = new System.Windows.Forms.Label();
            this.comboBoxAudioCues = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBoxStatus = new System.Windows.Forms.GroupBox();
            this.labelStatusAdmin = new System.Windows.Forms.Label();
            this.labelStatusProcess = new System.Windows.Forms.Label();
            this.labelStatusController = new System.Windows.Forms.Label();
            this.labelComingSoon = new System.Windows.Forms.Label();
            this.buttonRetryController = new System.Windows.Forms.Button();
            this.groupBoxKeyBinds.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelSettingsTitle
            // 
            this.labelSettingsTitle.AutoSize = true;
            this.labelSettingsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSettingsTitle.Location = new System.Drawing.Point(587, 21);
            this.labelSettingsTitle.Name = "labelSettingsTitle";
            this.labelSettingsTitle.Size = new System.Drawing.Size(197, 55);
            this.labelSettingsTitle.TabIndex = 0;
            this.labelSettingsTitle.Text = "Settings";
            // 
            // groupBoxKeyBinds
            // 
            this.groupBoxKeyBinds.Controls.Add(this.labelComingSoon);
            this.groupBoxKeyBinds.Location = new System.Drawing.Point(12, 12);
            this.groupBoxKeyBinds.Name = "groupBoxKeyBinds";
            this.groupBoxKeyBinds.Size = new System.Drawing.Size(552, 121);
            this.groupBoxKeyBinds.TabIndex = 1;
            this.groupBoxKeyBinds.TabStop = false;
            this.groupBoxKeyBinds.Text = "Key Binds";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRetryController);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBoxStartup);
            this.groupBox1.Controls.Add(this.checkBoxController);
            this.groupBox1.Controls.Add(this.labelAudioCues);
            this.groupBox1.Controls.Add(this.comboBoxAudioCues);
            this.groupBox1.Location = new System.Drawing.Point(12, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(552, 130);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Other Settings";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(280, 92);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(266, 24);
            this.checkBox3.TabIndex = 4;
            this.checkBox3.Text = "Automatically Check for Updates";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBoxStartup
            // 
            this.checkBoxStartup.AutoSize = true;
            this.checkBoxStartup.Location = new System.Drawing.Point(280, 62);
            this.checkBoxStartup.Name = "checkBoxStartup";
            this.checkBoxStartup.Size = new System.Drawing.Size(170, 24);
            this.checkBoxStartup.TabIndex = 3;
            this.checkBoxStartup.Text = "Start with Windows";
            this.checkBoxStartup.UseVisualStyleBackColor = true;
            // 
            // checkBoxController
            // 
            this.checkBoxController.AutoSize = true;
            this.checkBoxController.Location = new System.Drawing.Point(280, 32);
            this.checkBoxController.Name = "checkBoxController";
            this.checkBoxController.Size = new System.Drawing.Size(218, 24);
            this.checkBoxController.TabIndex = 2;
            this.checkBoxController.Text = "Enable Controller Support";
            this.checkBoxController.UseVisualStyleBackColor = true;
            // 
            // labelAudioCues
            // 
            this.labelAudioCues.AutoSize = true;
            this.labelAudioCues.Location = new System.Drawing.Point(6, 35);
            this.labelAudioCues.Name = "labelAudioCues";
            this.labelAudioCues.Size = new System.Drawing.Size(91, 20);
            this.labelAudioCues.TabIndex = 1;
            this.labelAudioCues.Text = "Audio Cues";
            // 
            // comboBoxAudioCues
            // 
            this.comboBoxAudioCues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAudioCues.FormattingEnabled = true;
            this.comboBoxAudioCues.Items.AddRange(new object[] {
            "Disabled",
            "Beeps",
            "Text-to-speech"});
            this.comboBoxAudioCues.Location = new System.Drawing.Point(103, 30);
            this.comboBoxAudioCues.Name = "comboBoxAudioCues";
            this.comboBoxAudioCues.Size = new System.Drawing.Size(157, 28);
            this.comboBoxAudioCues.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(597, 88);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(79, 45);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(695, 88);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(74, 45);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "Cancel";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // groupBoxStatus
            // 
            this.groupBoxStatus.Controls.Add(this.labelStatusAdmin);
            this.groupBoxStatus.Controls.Add(this.labelStatusProcess);
            this.groupBoxStatus.Controls.Add(this.labelStatusController);
            this.groupBoxStatus.Location = new System.Drawing.Point(570, 139);
            this.groupBoxStatus.Name = "groupBoxStatus";
            this.groupBoxStatus.Size = new System.Drawing.Size(214, 133);
            this.groupBoxStatus.TabIndex = 5;
            this.groupBoxStatus.TabStop = false;
            this.groupBoxStatus.Text = "Status";
            // 
            // labelStatusAdmin
            // 
            this.labelStatusAdmin.AutoSize = true;
            this.labelStatusAdmin.Location = new System.Drawing.Point(10, 80);
            this.labelStatusAdmin.Name = "labelStatusAdmin";
            this.labelStatusAdmin.Size = new System.Drawing.Size(147, 20);
            this.labelStatusAdmin.TabIndex = 2;
            this.labelStatusAdmin.Text = "Running as Admin: ";
            // 
            // labelStatusProcess
            // 
            this.labelStatusProcess.AutoSize = true;
            this.labelStatusProcess.Location = new System.Drawing.Point(10, 30);
            this.labelStatusProcess.Name = "labelStatusProcess";
            this.labelStatusProcess.Size = new System.Drawing.Size(111, 20);
            this.labelStatusProcess.TabIndex = 1;
            this.labelStatusProcess.Text = "GTA Process: ";
            // 
            // labelStatusController
            // 
            this.labelStatusController.AutoSize = true;
            this.labelStatusController.Location = new System.Drawing.Point(10, 55);
            this.labelStatusController.Name = "labelStatusController";
            this.labelStatusController.Size = new System.Drawing.Size(85, 20);
            this.labelStatusController.TabIndex = 0;
            this.labelStatusController.Text = "Controller: ";
            // 
            // labelComingSoon
            // 
            this.labelComingSoon.AutoSize = true;
            this.labelComingSoon.Location = new System.Drawing.Point(209, 56);
            this.labelComingSoon.Name = "labelComingSoon";
            this.labelComingSoon.Size = new System.Drawing.Size(106, 20);
            this.labelComingSoon.TabIndex = 0;
            this.labelComingSoon.Text = "Coming soon!";
            // 
            // buttonRetryController
            // 
            this.buttonRetryController.Location = new System.Drawing.Point(10, 72);
            this.buttonRetryController.Name = "buttonRetryController";
            this.buttonRetryController.Size = new System.Drawing.Size(250, 36);
            this.buttonRetryController.TabIndex = 5;
            this.buttonRetryController.Text = "Retry Controller Connection";
            this.buttonRetryController.UseVisualStyleBackColor = true;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 284);
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxStatus);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxKeyBinds);
            this.Controls.Add(this.labelSettingsTitle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.ShowInTaskbar = false;
            this.Text = "Settings";
            this.groupBoxKeyBinds.ResumeLayout(false);
            this.groupBoxKeyBinds.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxStatus.ResumeLayout(false);
            this.groupBoxStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSettingsTitle;
        private System.Windows.Forms.GroupBox groupBoxKeyBinds;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBoxStatus;
        private System.Windows.Forms.ComboBox comboBoxAudioCues;
        private System.Windows.Forms.Label labelAudioCues;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBoxStartup;
        private System.Windows.Forms.CheckBox checkBoxController;
        private System.Windows.Forms.Label labelStatusAdmin;
        private System.Windows.Forms.Label labelStatusProcess;
        private System.Windows.Forms.Label labelStatusController;
        private System.Windows.Forms.Label labelComingSoon;
        private System.Windows.Forms.Button buttonRetryController;
    }
}