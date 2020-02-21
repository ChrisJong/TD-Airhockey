namespace AirHockey.InteractionLayer
{
    using System;
    using System.Windows.Forms;
    using Microsoft.Surface;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
// ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            // Disable the WinForms unhandled exception dialog.
            // SurfaceShell will notify the user.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

            // Apply Surface globalization settings
            GlobalizationSettings.ApplyToCurrentThread();

            using (var app = new EntryPoint())
            {
                app.Run();
            }
        }
    }
}

