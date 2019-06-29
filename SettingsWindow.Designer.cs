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
            this.SuspendLayout();
            // 
            // labelSettingsTitle
            // 
            this.labelSettingsTitle.AutoSize = true;
            this.labelSettingsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSettingsTitle.Location = new System.Drawing.Point(587, 12);
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
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(695, 88);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(74, 45);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "Cancel";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // groupBoxStatus
            // 
            this.groupBoxStatus.Location = new System.Drawing.Point(570, 139);
            this.groupBoxStatus.Name = "groupBoxStatus";
            this.groupBoxStatus.Size = new System.Drawing.Size(214, 133);
            this.groupBoxStatus.TabIndex = 5;
            this.groupBoxStatus.TabStop = false;
            this.groupBoxStatus.Text = "Status";
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
    }
}