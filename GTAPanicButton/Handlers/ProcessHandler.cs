using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GTAPanicButton
{
    internal class ProcessHandler
    {
        [Flags]
        private enum ThreadAccess
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
            Process[] gtaProcesses = Process.GetProcessesByName("GTA5"); // there's probably only going to be one instance

            foreach (Process gtaProcess in gtaProcesses)
            {
                foreach (ProcessThread thread in gtaProcess.Threads)
                {
                    IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);

                    if (pOpenThread == IntPtr.Zero)
                        continue;

                    SuspendThread(pOpenThread);
                    CloseHandle(pOpenThread);
                }
            }
        }

        public static void ResumeProcess()
        {
            Process[] gtaProcesses = Process.GetProcessesByName("GTA5");
            foreach (Process gtaProcess in gtaProcesses)
            {
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
        }

        public static void KillGTASocialClubProcess()
        {
            try
            {
                KillProcess("GTA5");
                KillProcess("Launcher");
                KillProcess("SocialClubHelper");
                KillProcess("RockstarService");
            }
            catch (Exception e)
            {
                if (e is IndexOutOfRangeException) //this shouldnt trigger anymore
                {
                    MessageBox.Show("A process could not be found. You can " +
                                    "probably ignore this error.", "Warning",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (e is Win32Exception) //if it cant kill the process (most likely wont happen aswell unless gta is installed somewhere weird.)
                {
                    MessageBox.Show("Could not terminate the process. " +
                        "Try relaunching in adminstrator mode" +
                "then please make an issue on the Github " +
                "repository and include this error " +
                "message as a screenshot. Exception: " +
                e.Message, "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
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

        public static bool CheckProcess()
        {
            try
            {
                Process[] process = Process.GetProcessesByName("GTA5");
                if (process.Length <= 0)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void KillProcess(string ProcessName)
        {
            Process[] processes = Process.GetProcessesByName(ProcessName);
            if (processes.Length > 0)
            {
                foreach (Process proc in processes)
                {
                    proc.Kill();
                }
            }
        }
    }
}