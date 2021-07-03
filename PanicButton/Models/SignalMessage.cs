using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanicButton.Models
{
    /// <summary>
    /// Class that holds data about in-app messages.
    /// </summary>
    public class SignalMessage
    {
        public SignalMessageEnums.ProcessStatus processStatus;
        public SignalMessageEnums.ControllerStatus controllerStatus;
        public SignalMessageEnums.KeybindStatus keybindStatus;
        /// <summary>
        /// Holds an exception if thrown. To be handled by the subscriber(s).
        /// </summary>
        public Exception exception;

        public int? progress;
    }

    public static class SignalMessageEnums
    {
        /// <summary>
        /// Used to show game process status.
        /// </summary>
        public enum ProcessStatus
        {
            None,
            Suspended,
            Resumed,
            Terminated,
            SuspendError,
            ResumeError,
            TerminationError
        }

        /// <summary>
        /// Used to show controller status.
        /// </summary>
        public enum ControllerStatus
        {
            None,
            ControllerDetected,
            ControllerRemoved,
            ControllerSuspendPress,
            ControllerTerminatePress
        }

        /// <summary>
        /// Used to show keybind press status.
        /// </summary>
        public enum KeybindStatus
        {
            None,
            KeybindSuspendPress,
            KeybindTerminatePress
        }
    }
}
