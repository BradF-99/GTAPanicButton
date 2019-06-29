using System;
using System.Linq;
using System.Windows.Forms;

namespace GTAPanicButton
{
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();
            checkBoxStartup.Checked = MainWindow.startupRegKey.GetValueNames().Contains("GTA Panic Button") ? true : false;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxStartup.Checked)
                    MainWindow.startupRegKey.SetValue("GTA Panic Button", "\"" + Application.ExecutablePath + "\" /nocheck /hide");
                else
                    MainWindow.startupRegKey.DeleteValue("GTA Panic Button", true);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    MessageBox.Show("The registry key was not found. Maybe it was deleted already.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    string msgModifier;
                    if (checkBoxStartup.Checked)
                        msgModifier = "creating";
                    else
                        msgModifier = "deleting";

                    MessageBox.Show("We had a problem " + msgModifier + " the registry key." +
                                "Maybe try restarting the program as administrator.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    return;
                }
            }

            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
