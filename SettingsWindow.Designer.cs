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
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBoxStatus = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelAudioCues = new System.Windows.Forms.Label();
            this.checkBoxController = new System.Windows.Forms.CheckBox();
            this.checkBoxStartup = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.buttonRetryControllerConnection = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.groupBoxKeyBinds.Location = new System.Drawing.Point(12, 12);
            this.groupBoxKeyBinds.Name = "groupBoxKeyBinds";
            this.groupBoxKeyBinds.Size = new System.Drawing.Size(552, 121);
            this.groupBoxKeyBinds.TabIndex = 1;
            this.groupBoxKeyBinds.TabStop = false;
            this.groupBoxKeyBinds.Text = "Key Binds";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRetryControllerConnection);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBoxStartup);
            this.groupBox1.Controls.Add(this.checkBoxController);
            this.groupBox1.Controls.Add(this.labelAudioCues);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(552, 130);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Other Settings";
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
            this.groupBoxStatus.Controls.Add(this.label3);
            this.groupBoxStatus.Controls.Add(this.label2);
            this.groupBoxStatus.Controls.Add(this.label1);
            this.groupBoxStatus.Location = new System.Drawing.Point(570, 139);
            this.groupBoxStatus.Name = "groupBoxStatus";
            this.groupBoxStatus.Size = new System.Drawing.Size(214, 133);
            this.groupBoxStatus.TabIndex = 5;
            this.groupBoxStatus.TabStop = false;
            this.groupBoxStatus.Text = "Status";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Disabled",
            "Beeps",
            "Text-to-speech"});
            this.comboBox1.Location = new System.Drawing.Point(103, 30);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(157, 28);
            this.comboBox1.TabIndex = 0;
            // 
            // labelAudioCues
            // 
            this.labelAudioCues.AutoSize = true;
            this.labelAudioCues.Location = new System.Drawing.Point(6, 33);
            this.labelAudioCues.Name = "labelAudioCues";
            this.labelAudioCues.Size = new System.Drawing.Size(91, 20);
            this.labelAudioCues.TabIndex = 1;
            this.labelAudioCues.Text = "Audio Cues";
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
            // buttonRetryControllerConnection
            // 
            this.buttonRetryControllerConnection.Location = new System.Drawing.Point(10, 74);
            this.buttonRetryControllerConnection.Name = "buttonRetryControllerConnection";
            this.buttonRetryControllerConnection.Size = new System.Drawing.Size(250, 32);
            this.buttonRetryControllerConnection.TabIndex = 5;
            this.buttonRetryControllerConnection.Text = "Retry Controller Connection";
            this.buttonRetryControllerConnection.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Controller: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "GTA Process: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Running as Admin: ";
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
            this.Name = "SettingsWindow";
            this.ShowInTaskbar = false;
            this.Text = "Settings";
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
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label labelAudioCues;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBoxStartup;
        private System.Windows.Forms.CheckBox checkBoxController;
        private System.Windows.Forms.Button buttonRetryControllerConnection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}