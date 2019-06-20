using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Media;

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
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        private static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        private static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        private bool isSuspended = false;
        private bool soundCues = false;

        public MainWindow()
        {
            InitializeComponent();

            // check if GTA process is running
            try
            {
                //Process process = Process.GetProcessesByName("GTA5")[0];
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

            // Keycodes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8 (add together to change modifier)
            // Ctrl + Shift = 6
            RegisterHotKey(this.Handle, hotkeyKill, 6, (int)Keys.F11);
            RegisterHotKey(this.Handle, hotkeySuspend, 6, (int)Keys.F12);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == hotkeyNum && m.WParam.ToInt32() == hotkeySuspend)
            {
                if (!isSuspended)
                {
                    isSuspended = true;
                    SuspendProcess();
                    if (soundCues)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            SystemSounds.Beep.Play();
                            Thread.Sleep(1000);
                        }
                        SystemSounds.Exclamation.Play();
                    }
                    else
                    {
                        Thread.Sleep(10000);
                    }
                    ResumeProcess();
                    isSuspended = false;
                }
            }
            else if (m.Msg == hotkeyNum && m.WParam.ToInt32() == hotkeyKill)
            {
                KillGTASocialClubProcess();
            }

            base.WndProc(ref m);
        }

        private static void SuspendProcess()
        {
            var gtaProcess = Process.GetProcessesByName("GTA5")[0]; // there's probably only going to be one instance

            if (gtaProcess.ProcessName == string.Empty)
                return;

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

            if (gtaProcess.ProcessName == string.Empty)
                return;

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
                SystemSounds.Beep.Play();
            }
            catch (Exception e)
            {
                if (e is IndexOutOfRangeException) {
                     MessageBox.Show("A process could not be found. You can " +
                                     "probably ignore this error.", "Error", 
                                     MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            MessageBox.Show("Build 14 - compiled on 20/06/19.\n\n" +
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
                SystemSounds.Beep.Play();
            }
            else
            {
                soundCues = false;
            }
        }
    }
}
