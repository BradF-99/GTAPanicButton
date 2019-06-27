using System;
using System.Collections.Concurrent;
using SharpDX.XInput;

namespace GTAPanicButton
{
    class ControllerHandler
    {
        readonly Controller controller;
        Gamepad gamepad;
        public bool connected = false;
        public float leftTrigger, rightTrigger;
        public ConcurrentDictionary<string, bool> buttonStatus = new ConcurrentDictionary<string, bool>(Environment.ProcessorCount, Environment.ProcessorCount * 2);

        public ControllerHandler()
        {
            controller = new Controller(UserIndex.One);
            connected = controller.IsConnected;
            buttonStatus.TryAdd("Exit", false);
            buttonStatus.TryAdd("Suspend", false);
        }

        // Call this method to update all class values
        public void Update()
        {
            if (!connected)
                return;

            gamepad = controller.GetState().Gamepad;
            
            leftTrigger = gamepad.LeftTrigger;
            rightTrigger = gamepad.RightTrigger;

            if (leftTrigger > 250 && rightTrigger > 250)
            {
                if (gamepad.Buttons == GamepadButtonFlags.LeftShoulder && gamepad.Buttons == GamepadButtonFlags.RightShoulder)
                {
                    buttonStatus.TryUpdate("Exit", false, true);
                }
                else if (gamepad.Buttons == GamepadButtonFlags.LeftThumb && gamepad.Buttons == GamepadButtonFlags.RightThumb)
                {
                    buttonStatus.TryUpdate("Suspend", false, true);
                }
            }
            else
            {
                buttonStatus.TryUpdate("Exit", true, false);
                buttonStatus.TryUpdate("Suspend", true, false);
            }
        }
    }
}
