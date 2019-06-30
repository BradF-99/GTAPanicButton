using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GTAPanicButton
{
    class KeybindHandler
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd,
                                                    int id,
                                                    int fsModifiers,
                                                    int vlc);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd,
                                                   int id);

        public const int hotkeySuspend = 1;
        public const int hotkeyKill = 2;

        // Keycodes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8 (add together to change modifier)
        // Ctrl + Shift = 6

        public static void RegisterHotkeys(MainWindow window)
        {
            RegisterHotKey(window.Handle, hotkeyKill, 6, (int)Keys.F11);
            RegisterHotKey(window.Handle, hotkeySuspend, 6, (int)Keys.F12);
        }

        public static void UnregisterHotkeys(MainWindow window)
        {
            UnregisterHotKey(window.Handle, hotkeyKill);
            UnregisterHotKey(window.Handle, hotkeySuspend);
        }
    }
}
