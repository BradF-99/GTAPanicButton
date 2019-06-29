using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel;
using System.Speech.Synthesis;
using System.Linq;
using Microsoft.Win32;

namespace GTAPanicButton
{
    public partial class MainWindow : Form
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd,
                                                 int id,
                                                 int fsModifiers,
                                                 int vlc);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd,
                                                   int id);

        private const int hotkeyNum = 0x0312; // WM_HOTKEY
        private const int hotkeySuspend = 1;
        private const int hotkeyKill = 2;

        private bool soundCues = false;
        private bool balloonStatus = false; // set to false when balloon is shown (initialised true during window creation for hide arg)
        private readonly bool processCheckFlag = true; // set to false if nocheck arg is passed
       
        private readonly SpeechSynthesizer speech;
        private readonly RegistryKey startupRegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        private readonly ControllerHandler controller = new ControllerHandler();
        private IDictionary<string, bool> controllerStatus;

        public MainWindow(string[] args)
        {
            InitializeComponent();
            notifyIcon.Visible = false;

            // check if GTA process is running
            if(args.Length > 0) // high iq arg handler
            {
                foreach(string arg in args)
                {
                    switch (arg)
                    {
                        case "/nocheck": // don't perform check for process 
                            processCheckFlag = false;
                            break;
                        case "/hide": // start in systray
                            notifyIcon.Visible = true;
                            this.WindowState = FormWindowState.Minimized;
                            this.Hide();
                            this.Visible = false; // for some reason Hide() doesn't always work
                            this.ShowInTaskbar = false;
                            break;
                    }
                }
            }

            if (processCheckFlag)
                ProcessHandler.CheckProcess();

            speech = new SpeechSynthesizer();
            speech.SetOutputToDefaultAudioDevice();
            
            // Keycodes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8 (add together to change modifier)
            // Ctrl + Shift = 6
            RegisterHotKey(this.Handle, hotkeyKill, 6, (int)Keys.F11);
            RegisterHotKey(this.Handle, hotkeySuspend, 6, (int)Keys.F12);

            InitialiseBackgroundWorkers();

            checkBoxStartup.Checked = startupRegKey.GetValueNames().Contains("GTA Panic Button") ? true : false;

            balloonStatus = true;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == hotkeyNum && m.WParam.ToInt32() == hotkeySuspend)
            {
                if (!processSuspendWorker.IsBusy)
                    processSuspendWorker.RunWorkerAsync();
            }
            else if (m.Msg == hotkeyNum && m.WParam.ToInt32() == hotkeyKill)
            {
                if(!processDestroyWorker.IsBusy)
                    processDestroyWorker.RunWorkerAsync();
            }

            base.WndProc(ref m);
        }
        
        private void InitialiseBackgroundWorkers()
        {
            processSuspendWorker.DoWork +=
                new DoWorkEventHandler(ProcessSuspendWorker_DoWork);
            processSuspendWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(ProcessSuspendWorker_RunWorkerCompleted);
            processSuspendWorker.ProgressChanged +=
                new ProgressChangedEventHandler(ProcessSuspendWorker_ProgressChanged);
            processSuspendWorker.WorkerReportsProgress = true;

            processDestroyWorker.DoWork +=
                new DoWorkEventHandler(ProcessDestroyWorker_DoWork);
            processDestroyWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(ProcessDestroyWorker_RunWorkerCompleted);
            processDestroyWorker.WorkerReportsProgress = true;

            controllerWorker.DoWork +=
                new DoWorkEventHandler(ControllerWorker_DoWork);
            controllerWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(ControllerWorker_RunWorkerCompleted);
            controllerWorker.WorkerReportsProgress = true;
            controllerWorker.WorkerSupportsCancellation = true;

            if (controller.connected)
                controllerWorker.RunWorkerAsync();
        }

        private void ProcessSuspendWorker_DoWork(object sender, DoWorkEventArgs args)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                ProcessHandler.SuspendProcess();

                if (soundCues)
                    speech.SpeakAsync("GTA suspended, one moment.");

                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    worker.ReportProgress((i + 1) * 10);
                }

                ProcessHandler.ResumeProcess();

                if (soundCues)
                    speech.SpeakAsync("GTA resumed.");

                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                if (e is IndexOutOfRangeException)
                {
                    MessageBox.Show("A process could not be found. Is GTA running?", "Warning",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Something went wrong. " +
                                    "Please make an issue on the Github " +
                                    "repository and include this error " +
                                    "message as a screenshot. Exception: " +
                                    e.Message, "Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void ProcessSuspendWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarTimer.Value = e.ProgressPercentage;
        }

        private void ProcessSuspendWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarTimer.Value = 0;
            if(e.Error != null)
            {
                MessageBox.Show("Something went wrong. " +
                            "Please make an issue on the Github " +
                            "repository and include this error " +
                            "message as a screenshot. Exception: " +
                            e.Error.Message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            }
        }

        private void ProcessDestroyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ProcessHandler.KillGTASocialClubProcess();
            if (soundCues)
                speech.SpeakAsync("GTA processes destroyed.");
            Thread.Sleep(5000); // stops it from triggering again
        }

        private void ProcessDestroyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Something went wrong. " +
                            "Please make an issue on the Github " +
                            "repository and include this error " +
                            "message as a screenshot. Exception: " +
                            e.Error.Message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            }
        }

        private void ControllerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (controller.connected)
            {
                try
                {
                    controllerStatus = controller.Update();
                }
                catch (SharpDX.SharpDXException)
                {
                    // fail silently, just means the controller was disconnected during poll
                }
            }
            else
            {
                controllerWorker.CancelAsync();
            }
            Thread.Sleep(100); // precision not required so no point polling at 100mhz
        }

        private void ControllerWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Something went wrong. " +
                            "Please make an issue on the Github " +
                            "repository and include this error " +
                            "message as a screenshot. Exception: " +
                            e.Error.Message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            }

            if (!controllerWorker.CancellationPending) { 
                if (!controllerStatus.Equals(null) || !controller.connected)
                {
                    if (controllerStatus["Suspend"])
                    {
                        if (processSuspendWorker.IsBusy != true)
                            processSuspendWorker.RunWorkerAsync();
                    }
                    else if (controllerStatus["Exit"])
                    {
                        if (processDestroyWorker.IsBusy != true)
                            processDestroyWorker.RunWorkerAsync();
                    }
                    controllerWorker.RunWorkerAsync();
                }
            }
            else
            {
                controllerWorker.CancelAsync();
            }
        }

        private void BtnCredits_Click(object sender, EventArgs e)
        {
            MessageBox.Show("v1.27 (Build 27) - compiled on 2019/06/26.\n\n" +
                            "Developers: BradF-99 & Assasindie\n" +
                            "Testers: joco & charlco\n" +
                            "Thank you to the testers, as well as " +
                            "Magnus Johansson, Otiel and henon " +
                            "on StackOverflow!",
                            "Credits",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void CheckboxBeep_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxBeep.Checked)
            {
                soundCues = true;
                speech.SpeakAsync("Hello!");
            }
            else
            {
                soundCues = false;
            }
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon.Visible = true;
                if (balloonStatus)
                {
                    notifyIcon.ShowBalloonTip(7000); // 7 seconds
                }
                this.Hide();
                this.ShowInTaskbar = false;
                this.Visible = false;
                balloonStatus = false;
            }
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
            this.ShowInTaskbar = true;
            this.Visible = true;
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e) // clean up after ourselves
        {
            UnregisterHotKey(this.Handle, hotkeyKill);
            UnregisterHotKey(this.Handle, hotkeySuspend);
        }

        private void CheckBoxStartup_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxStartup.Checked)
                    startupRegKey.SetValue("GTA Panic Button", "\"" + Application.ExecutablePath + "\" /nocheck /hide");
                else
                    startupRegKey.DeleteValue("GTA Panic Button", true);
            }
            catch (Exception ex)
            {
                if(ex is ArgumentException)
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
                        
                    MessageBox.Show("We had a problem "+ msgModifier +" the registry key." +
                                "Maybe try restarting the program as administrator.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    return;
                }
            }
        }
    }
}
