using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using PanicButton.Models;
using SharpDX.XInput;

namespace PanicButton.Services
{
    public class XInputService
    {
        private readonly Controller controller;
        private Gamepad gamepad;
        public float leftTrigger, rightTrigger;

        private readonly Timer timerDisconnected;
        private readonly Timer timerConnected;

        public XInputService()
        {
            controller = new Controller(UserIndex.One);

            timerConnected = new(100);
            timerConnected.Elapsed += TimerConnected_Elapsed;
            timerDisconnected = new(1000);
            timerDisconnected.Elapsed += TimerDisconnected_Elapsed;

            if (controller.IsConnected)
                timerConnected.Start();
            else
                timerDisconnected.Start();
        }

        internal void TimerConnected_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!controller.IsConnected)
            {
                timerConnected.Stop();
                timerDisconnected.Start();
                EventAggregator.Instance.Publish(new SignalMessage()
                {
                    controllerStatus = SignalMessageEnums.ControllerStatus.ControllerRemoved
                });
            }
            else
            {
                gamepad = controller.GetState().Gamepad;

                leftTrigger = gamepad.LeftTrigger;
                rightTrigger = gamepad.RightTrigger;

                if (leftTrigger > 250 && rightTrigger > 250)
                {
                    if (gamepad.Buttons == GamepadButtonFlags.LeftThumb)
                    {
                        ProcessService.KillGTASocialClubProcess();
                    }
                    else if (gamepad.Buttons == GamepadButtonFlags.RightThumb)
                    {
                        ProcessService.Instance.SuspendGTAProcess();
                    }
                }
            }
        }

        internal void TimerDisconnected_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (controller.IsConnected)
            {
                timerDisconnected.Stop();
                timerConnected.Start();
                EventAggregator.Instance.Publish(new SignalMessage()
                {
                    controllerStatus = SignalMessageEnums.ControllerStatus.ControllerDetected
                });
            }
        }
    }
}
