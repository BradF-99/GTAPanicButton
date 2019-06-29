using System;
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
        public IDictionary<string, bool> buttonStatus;

        public ControllerHandler()
        {
            controller = new Controller(UserIndex.One);
            connected = controller.IsConnected;
            buttonStatus = new Dictionary<string, bool>
            {
                { "Exit", false },
                { "Suspend", false }
            };
        }

        // Call this method to update all class values
        public IDictionary<string, bool> Update()
        {
            connected = controller.IsConnected; // should fix error thrown when controller disconnects

            gamepad = controller.GetState().Gamepad;
            
            leftTrigger = gamepad.LeftTrigger;
            rightTrigger = gamepad.RightTrigger;

            if (leftTrigger > 250 && rightTrigger > 250)
            {
                if (gamepad.Buttons == GamepadButtonFlags.LeftThumb)
                {
                    buttonStatus["Exit"] = true;
                }
                else if (gamepad.Buttons == GamepadButtonFlags.RightThumb)
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
