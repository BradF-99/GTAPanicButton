using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using System.Speech.Synthesis;
using Microsoft.Win32;
using System.Linq;

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

        [Flags]
        private enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002)
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        private static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        private static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        private bool soundCues = false;
        private bool balloonStatus = false; // set to false when balloon is shown (initialised true during window creation for hide arg)
        private bool processCheckFlag = true; // set to false if nocheck arg is passed

        private readonly SpeechSynthesizer speech;

        private readonly OperatingSystem osInfo = System.Environment.OSVersion;

        private RegistryKey startupRegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);


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
                CheckProcess();

            speech = new SpeechSynthesizer();
            speech.SetOutputToDefaultAudioDevice();

            // Keycodes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8 (add together to change modifier)
            // Ctrl + Shift = 6
            RegisterHotKey(this.Handle, hotkeyKill, 6, (int)Keys.F11);
            RegisterHotKey(this.Handle, hotkeySuspend, 6, (int)Keys.F12);

            InitialiseBackgroundWorker();

            checkBoxStartup.Checked = startupRegKey.GetValueNames().Contains("GTA Panic Button") ? true : false;

            balloonStatus = true;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == hotkeyNum && m.WParam.ToInt32() == hotkeySuspend)
            {
                if (backgroundWorker.IsBusy != true)
                {
                    backgroundWorker.RunWorkerAsync();
                }
            }
            else if (m.Msg == hotkeyNum && m.WParam.ToInt32() == hotkeyKill)
            {
                KillGTASocialClubProcess();
                if (soundCues)
                    speech.SpeakAsync("GTA processes destroyed.");
            }

            base.WndProc(ref m);
        }

        private void InitialiseBackgroundWorker()
        {
            backgroundWorker.DoWork +=
                new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker_RunWorkerCompleted);
            backgroundWorker.ProgressChanged +=
                new ProgressChangedEventHandler(
            backgroundWorker_ProgressChanged);

            backgroundWorker.WorkerReportsProgress = true;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs args)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                SuspendProcess();

                if (soundCues)
                    speech.SpeakAsync("GTA suspended, one moment.");

                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    worker.ReportProgress((i + 1) * 10);
                }

                ResumeProcess();

                if (soundCues)
                    speech.SpeakAsync("GTA resumed.");

                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                if (e is IndexOutOfRangeException)
                {
                    MessageBox.Show("A process could not be found. You can " +
                                    "probably ignore this error.", "Warning",
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

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarTimer.Value = e.ProgressPercentage;
        }
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

        private static void SuspendProcess()
        {
            var gtaProcess = Process.GetProcessesByName("GTA5")[0]; // there's probably only going to be one instance

            foreach (ProcessThread thread in gtaProcess.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                SuspendThread(pOpenThread);
                CloseHandle(pOpenThread);
            }
        }

        private static void ResumeProcess()
        {
            Process gtaProcess = Process.GetProcessesByName("GTA5")[0];

            foreach (ProcessThread thread in gtaProcess.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                var suspendCount = 0;
                do
                {
                    suspendCount = ResumeThread(pOpenThread);
                } while (suspendCount > 0);

                CloseHandle(pOpenThread);
            }
        }

        private static void KillGTASocialClubProcess()
        {
            try
            {
                Process gtaProcess = Process.GetProcessesByName("GTA5")[0];
                gtaProcess.Kill();

                Process gtaLauncherProcess = Process.GetProcessesByName("GTAVLauncher")[0];
                gtaLauncherProcess.Kill();

                Process[] socialClubProcesses = Process.GetProcessesByName("SocialClubHelper");
                if (socialClubProcesses.Length <= 0)
                    return;

                foreach (Process process in socialClubProcesses)
                {
                    process.Kill();
                }
            }
            catch (Exception e)
            {
                if (e is IndexOutOfRangeException) {
                     MessageBox.Show("A process could not be found. You can " +
                                     "probably ignore this error.", "Warning", 
                                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } else {
                     MessageBox.Show("Something went wrong. " +
                                     "Please make an issue on the Github " +
                                     "repository and include this error " +
                                     "message as a screenshot. Exception: " +
                                     e.Message, "Error", MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);
                }
            }
        }

        private static void CheckProcess()
        {
            try
            {
                Process process = Process.GetProcessesByName("GTA5")[0];
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("The GTA game process could not be found. " +
                                "Maybe try relaunching this program as an administrator.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                Environment.Exit(1);
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
                    // thanks microsoft / windows 10
                    // i refuse to import UWP and the Windows 10 SDK just for a single notification

                    if (osInfo.Version.Major == 6 && osInfo.Version.Minor == 2 && osInfo.Version.Build == 9200) 
                    {
                        MessageBox.Show("The GTA Panic Button will minimise to your task bar.\n" +
                            "Click on the icon in the task bar to maximise it again.\n" +
                            "To close, click this icon and close the program.\n\n" +
                            "Usually this message would show as a balloon pop-up but " +
                            "Windows 10 doesn't do those anymore.", "Quick Message for Windows 10 users",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        notifyIcon.ShowBalloonTip(7000); // 7 seconds
                    }
                }
                this.Hide();
                this.ShowInTaskbar = false;
                this.Visible = false;
                balloonStatus = false;
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
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
