using Sentry;
using System;
using System.Windows.Forms;

namespace GTAPanicButton
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            using (SentrySdk.Init(app secret lol!))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow(args));
            }
        }
    }
}