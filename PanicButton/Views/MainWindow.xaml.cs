using System;
using System.Windows;
using System.Windows.Interop;
using PanicButton.Services;

namespace PanicButton.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HwndSource _source;
        private WindowInteropHelper helper;
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            helper = new WindowInteropHelper(this);
            _source = HwndSource.FromHwnd(helper.Handle);
            _source.AddHook(KeybindService.HwndHook);
            KeybindService.RegisterHotKey(helper);
        }
        protected override void OnClosed(EventArgs e)
        {
            _source.RemoveHook(KeybindService.HwndHook);
            _source = null;
            KeybindService.UnregisterHotKey(helper);
            base.OnClosed(e);
        }
    }
}
