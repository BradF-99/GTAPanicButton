using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SharpDX.XInput;

namespace GTAPanicButton
{
    class ControllerHandler
    {
        readonly Controller controller;
        Gamepad gamepad;
        public bool connected = false;
        public float leftTrigger, rightTrigger;
        public IDictionary<string, bool> buttonStatus = new Dictionary<string, bool>();

        public ControllerHandler()
        {
            controller = new Controller(UserIndex.One);
            connected = controller.IsConnected;
            buttonStatus.Add("Exit", false);
            buttonStatus.Add("Suspend", false);
        }

        // Call this method to update all class values
        public IDictionary<string, bool> Update()
        {
            gamepad = controller.GetState().Gamepad;
            
            leftTrigger = gamepad.LeftTrigger;
            rightTrigger = gamepad.RightTrigger;

            if (leftTrigger > 250 && rightTrigger > 250)
            {
                if (gamepad.Buttons == GamepadButtonFlags.LeftShoulder)
                {
                    buttonStatus["Exit"] = true;
                }
                else if (gamepad.Buttons == GamepadButtonFlags.RightShoulder)
                {
                    buttonStatus["Suspend"] = true;
                }
            }
            else
            {
                buttonStatus["Exit"] = false;
                buttonStatus["Suspend"] = false;
            }

            return buttonStatus;
        }
    }
}
