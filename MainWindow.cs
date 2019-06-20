using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using System.Speech.Synthesis;

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

        private const int hotkeyNum = 0x0312;
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

        private readonly SpeechSynthesizer speech;

        public MainWindow()
        {
            InitializeComponent();

            // check if GTA process is running
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

            speech = new SpeechSynthesizer();
            speech.SetOutputToDefaultAudioDevice();

            // Keycodes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8 (add together to change modifier)
            // Ctrl + Shift = 6
            RegisterHotKey(this.Handle, hotkeyKill, 6, (int)Keys.F11);
            RegisterHotKey(this.Handle, hotkeySuspend, 6, (int)Keys.F12);

            InitialiseBackgroundWorker();
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
                Process gtaLauncherProcess = Process.GetProcessesByName("GTAVLauncher")[0];
                Process[] socialClubProcesses = Process.GetProcessesByName("SocialClubHelper");

                gtaProcess.Kill();
                gtaLauncherProcess.Kill();

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

        private void BtnCredits_Click(object sender, EventArgs e)
        {
            MessageBox.Show("v1.23 - compiled on 21/06/19.\n\n" +
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
    }
}
