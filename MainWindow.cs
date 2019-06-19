using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace GTAPanicButton
{
    public partial class MainWindow : Form
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd,
                                                 int id,
                                                 int fsModifiers,
                                                 int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd,
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
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);

        private bool isSuspended = false; // this will stop it from trying to suspend twice

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                Process process = Process.GetProcessesByName("GTA5")[0];
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("The GTA game process could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            // Keycodes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8 (add together to change modifier)
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
                    Thread.Sleep(10000);
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

        public static void ResumeProcess()
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

        public static void KillGTASocialClubProcess()
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
            catch (NotSupportedException)
            {
                // this shouldnt even happen???
            }
            catch (InvalidOperationException)
            {
                // process has already exited or doesn't exist
            }
            catch
            {
                MessageBox.Show("Something went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
    }
}
