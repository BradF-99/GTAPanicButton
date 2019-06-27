using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX.XInput;

namespace GTAPanicButton
{
    class ControllerInput
    {
        Controller controller;
        Gamepad gamepad;
        public bool connected = false;
        public float leftTrigger, rightTrigger;

        public ControllerInput()
        {
            controller = new Controller(UserIndex.One);
            connected = controller.IsConnected;
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
                    // quit game
                }
                else if (gamepad.Buttons == GamepadButtonFlags.LeftThumb && gamepad.Buttons == GamepadButtonFlags.RightThumb)
                {
                    // suspend game
                }
            }
        }
    }
}
