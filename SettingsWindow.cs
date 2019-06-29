using System;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace GTAPanicButton
{
    public partial class SettingsWindow : Form
    {

        public SettingsWindow(bool gtaProcessDetected, bool controllerConnected)
        {
            InitializeComponent();

            checkBoxStartup.Checked = MainWindow.startupRegKey.GetValueNames().Contains("GTA Panic Button") ? true : false;

            if (gtaProcessDetected)
                labelStatusProcess.Text += "Detected";
            else
                labelStatusProcess.Text += "Unknown"; // check bypassed

            if (controllerConnected)
                labelStatusController.Text += "Connected";
            else
                labelStatusController.Text += "Disconnected";

            if (IsAdministrator())
                labelStatusAdmin.Text += "Yes";
            else
                labelStatusAdmin.Text += "No";

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

        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
