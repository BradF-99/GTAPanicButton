using System;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace GTAPanicButton
{
    public partial class SettingsWindow : Form
    {
        public SettingsWindow(bool controllerConnected)
        {
            InitializeComponent();

            checkBoxStartup.Checked = MainWindow.startupRegKey.GetValueNames().Contains("GTA Panic Button");
            checkBoxController.Checked = Properties.Settings.Default.controllerSupport;

            if (controllerConnected)
                labelStatusController.Text += "Connected";
            else
                labelStatusController.Text += "Disconnected";

            if (IsAdministrator())
                labelStatusAdmin.Text += "Yes";
            else
                labelStatusAdmin.Text += "No";

            if (Properties.Settings.Default.soundCuesBeep)
                comboBoxAudioCues.SelectedIndex = 1;
            else if (Properties.Settings.Default.soundCuesTTS)
                comboBoxAudioCues.SelectedIndex = 2;
            else
                comboBoxAudioCues.SelectedIndex = 0;
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
                else if (!checkBoxStartup.Checked && MainWindow.startupRegKey.GetValueNames().Contains("GTA Panic Button"))
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

            if (comboBoxAudioCues.SelectedIndex == 0)
            {
                Properties.Settings.Default.soundCuesBeep = false;
                Properties.Settings.Default.soundCuesTTS = false;
            }
            else if (comboBoxAudioCues.SelectedIndex == 1)
            {
                Properties.Settings.Default.soundCuesBeep = true;
                Properties.Settings.Default.soundCuesTTS = false;
            }
            else if (comboBoxAudioCues.SelectedIndex == 2)
            {
                Properties.Settings.Default.soundCuesBeep = false;
                Properties.Settings.Default.soundCuesTTS = true;
            }

            Properties.Settings.Default.controllerSupport = checkBoxController.Checked;

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