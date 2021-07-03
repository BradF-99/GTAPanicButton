using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using PanicButton.Models;
using Sentry;
using Timer = System.Timers.Timer;

namespace PanicButton.Services
{
    public sealed class ProcessService
    {
        private readonly Timer timer;
        private int intervalsElapsed;
        private bool suspended;

        private ProcessService()
        {
            timer = new Timer(1000) {AutoReset = false};
            timer.Elapsed += Timer_Elapsed;
            intervalsElapsed = 0;
            suspended = false;
        }

        private static readonly Lazy<ProcessService> lazy = new(() => new ProcessService());

        public static ProcessService Instance
        {
            get { return lazy.Value; }
        }

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

        private static void SuspendProcesses(string processName)
        {
            SuspendProcesses(Process.GetProcessesByName(processName)); // there's probably only going to be one instance

        }

        private static void SuspendProcesses(Process[] processes)
        {
            if (processes.Length == 0)
                throw new Exception("Process array empty.");

            foreach (Process process in processes)
            {
                foreach (ProcessThread thread in process.Threads)
                {
                    IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);

                    if (pOpenThread == IntPtr.Zero)
                        continue;

                    if (SuspendThread(pOpenThread) > 0)
                    {
                        throw new ThreadStateException("Unable to suspend thread.");
                    }
                    CloseHandle(pOpenThread);
                }
            }
        }

        private static void ResumeProcesses(string processName)
        {
            ResumeProcesses(Process.GetProcessesByName(processName));
        }

        private static void ResumeProcesses(Process[] processes)
        {
            if (processes.Length == 0)
                throw new Exception("Process array empty.");

            foreach (Process process in processes)
            {
                foreach (ProcessThread thread in process.Threads)
                {
                    IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);

                    if (pOpenThread == IntPtr.Zero)
                        continue;

                    int suspendCount;
                    do
                    {
                        suspendCount = ResumeThread(pOpenThread);
                    } while (suspendCount > 0);

                    CloseHandle(pOpenThread);
                }
            }
        }
        
        private static void KillProcess(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                foreach (Process proc in processes)
                {
                    proc.Kill();
                }
            }
        }

        internal static void KillGTASocialClubProcess()
        {
            try
            {
                KillProcess("GTA5");
                KillProcess("PlayGTAV");
                KillProcess("GTAVLanguageSelect");
                KillProcess("GTAVLauncher");

                KillProcess("Launcher");
                KillProcess("LauncherPatcher");
                KillProcess("SocialClubHelper");
                KillProcess("RockstarService");
                KillProcess("RockstarSteamHelper");

                SentrySdk.CaptureMessage("Game terminated.");

                EventAggregator.Instance.Publish(new SignalMessage()
                {
                    processStatus = SignalMessageEnums.ProcessStatus.Terminated
                });
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                EventAggregator.Instance.Publish(new SignalMessage()
                {
                    processStatus = SignalMessageEnums.ProcessStatus.TerminationError,
                    exception = ex,
                    progress = 0
                });
            }
        }

        internal void SuspendGTAProcess()
        {
            if (suspended)
                return;

            try
            {
                SuspendProcesses("GTA5");
                SentrySdk.CaptureMessage("Game suspended.");
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                EventAggregator.Instance.Publish(new SignalMessage()
                {
                    processStatus = SignalMessageEnums.ProcessStatus.SuspendError,
                    exception = ex,
                    progress = 0
                });
                intervalsElapsed = 0;
                suspended = false;
                return;
            }

            suspended = true;
            timer.Start();
            timer.AutoReset = true;
            EventAggregator.Instance.Publish(new SignalMessage()
            {
                processStatus = SignalMessageEnums.ProcessStatus.Suspended
            });
        }

        internal void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (intervalsElapsed == 10)
            {
                try
                {
                    ResumeProcesses("GTA5");
                    EventAggregator.Instance.Publish(new SignalMessage()
                    {
                        processStatus = SignalMessageEnums.ProcessStatus.Resumed,
                        progress = 0
                    });
                }
                catch (Exception ex)
                {
                    SentrySdk.CaptureException(ex);
                    EventAggregator.Instance.Publish(new SignalMessage()
                    {
                        processStatus = SignalMessageEnums.ProcessStatus.ResumeError,
                        exception = ex,
                        progress = 0
                    });
                }
                finally
                {
                    timer.AutoReset = false;
                    timer.Stop();
                    intervalsElapsed = 0;
                    suspended = false;
                }
            }
            else
            {
                EventAggregator.Instance.Publish(new SignalMessage()
                {
                    processStatus = SignalMessageEnums.ProcessStatus.Suspended,
                    progress = (intervalsElapsed + 1) * 10
                });
                intervalsElapsed++;
            }
        }
    }
}
