using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using Microsoft.Win32;

namespace GTAPanicButton
{
    public partial class MainWindow : Form
    {
        private const int hotkeyNum = 0x0312; // WM_HOTKEY

        private bool soundCuesBeep = Properties.Settings.Default.soundCuesBeep;  
        private bool soundCuesTTS = Properties.Settings.Default.soundCuesTTS;
        private bool balloonStatus = false; // set to false when balloon is shown (initialised true during window creation for hide arg)
        private readonly bool processCheckFlag = true; // set to false if nocheck arg is passed
       

        public static readonly RegistryKey startupRegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        private readonly ControllerHandler controller = new ControllerHandler();
        private IDictionary<string, bool> controllerStatus;

        private SoundHandler soundHandler = new SoundHandler();

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

            KeybindHandler.RegisterHotkeys(this);
            InitialiseBackgroundWorkers();

            balloonStatus = true;
            
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == hotkeyNum && m.WParam.ToInt32() == KeybindHandler.hotkeySuspend)
            {
                if (!processSuspendWorker.IsBusy)
                    processSuspendWorker.RunWorkerAsync();
            }
            else if (m.Msg == hotkeyNum && m.WParam.ToInt32() == KeybindHandler.hotkeyKill)
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

                if (soundCuesTTS)
                    soundHandler.speech.SpeakAsync("GTA suspended, one moment.");
                  
                for (int i = 0; i < 10; i++)
                {
                    if (soundCuesBeep)
                    {
                        soundHandler.PlayBeep(false);
                        Thread.Sleep(900); // sound will take 100ms
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                    worker.ReportProgress((i + 1) * 10);
                }

                ProcessHandler.ResumeProcess();

                if (soundCuesTTS)
                    soundHandler.speech.SpeakAsync("GTA resumed.");
                else if (soundCuesBeep)
                    soundHandler.PlayBeep(true);

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
            if (soundCuesTTS)
                soundHandler.speech.SpeakAsync("GTA processes destroyed.");
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
            MessageBox.Show("v1.53 (Build 53) - compiled on 2019/06/29.\n\n" +
                            "Developers: BradF-99 & Assasindie\n" +
                            "Testers: joco & charlco\n" +
                            "Thank you to the testers, as well as " +
                            "Magnus Johansson, Otiel and henon " +
                            "on StackOverflow!",
                            "Credits",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
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
            KeybindHandler.UnregisterHotkeys(this);
        }

        private void ButtonOptions_Click(object sender, EventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }
    }
}