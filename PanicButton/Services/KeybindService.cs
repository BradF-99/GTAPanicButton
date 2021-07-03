using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using PanicButton.Views;

namespace PanicButton.Services
{
    public static class KeybindService
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(
            [In] IntPtr hWnd,
            [In] int id,
            [In] uint fsModifiers,
            [In] uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(
            [In] IntPtr hWnd,
            [In] int id);

        private const int HotkeySuspend = 1;
        private const int HotkeyKill = 2;

        // Keycodes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8 (add together to change modifier)
        // Ctrl + Shift = 6
        // https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes

        public static void RegisterHotKey(WindowInteropHelper helper)
        {
            if (!RegisterHotKey(helper.Handle, HotkeyKill, 6, 0x7A))
            {
                // don't care lmao!
            }
            if (!RegisterHotKey(helper.Handle, HotkeySuspend, 6, 0x7B))
            {
                // don't care lmao!
            }
        }

        public static void UnregisterHotKey(WindowInteropHelper helper)
        {
            UnregisterHotKey(helper.Handle, HotkeyKill);
            UnregisterHotKey(helper.Handle, HotkeySuspend);
        }
        public static IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HotkeyKill:
                            ProcessService.KillGTASocialClubProcess();
                            handled = true;
                            break;
                        case HotkeySuspend:
                            ProcessService.Instance.SuspendGTAProcess();
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }
    }
}
