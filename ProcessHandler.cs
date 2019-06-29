using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.ComponentModel;

namespace GTAPanicButton
{
    class ProcessHandler
    {
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


        public static void SuspendProcess()
        {
            Process gtaProcess = Process.GetProcessesByName("GTA5")[0]; // there's probably only going to be one instance
            foreach (ProcessThread thread in gtaProcess.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);

                if (pOpenThread == IntPtr.Zero)
                    continue;

                SuspendThread(pOpenThread);
                CloseHandle(pOpenThread);
            }
        }

        public static void ResumeProcess()
        {
            Process gtaProcess = Process.GetProcessesByName("GTA5")[0];
            foreach (ProcessThread thread in gtaProcess.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);

                if (pOpenThread == IntPtr.Zero)
                    continue;

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
                Process[] gtaProcess = Process.GetProcessesByName("GTA5");
                if (gtaProcess.Length > 0)
                    foreach (Process process in gtaProcess)
                    {
                        process.Kill();
                    }

                Process[] gtaLauncherProcess = Process.GetProcessesByName("GTAVLauncher");
                if (gtaLauncherProcess.Length > 0)
                    foreach (Process process in gtaLauncherProcess)
                    {
                        process.Kill();
                    }

                Process[] socialClubProcesses = Process.GetProcessesByName("SocialClubHelper");
                if (socialClubProcesses.Length > 0)
                    foreach (Process process in socialClubProcesses)
                    {
                        process.Kill();
                    }

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

        public static void CheckProcess()
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
    }
}
