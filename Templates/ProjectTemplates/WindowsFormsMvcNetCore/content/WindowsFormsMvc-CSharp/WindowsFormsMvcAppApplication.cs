using System;
using System.Threading;
using System.Windows.Forms;
using Charites.Windows.Mvc;
using Microsoft.Extensions.Hosting;

namespace WindowsFormsMvcApp
{
    internal class WindowsFormsMvcAppApplication : ApplicationContext, IWindowsFormsMvcAppApplication
    {
        private readonly IHostApplicationLifetime lifetime;

        public WindowsFormsMvcAppApplication(IHostApplicationLifetime lifetime, IWindowsFormsControllerFactory controllerFactory)
        {
            this.lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));

            WindowsFormsController.UnhandledException += OnWindowsFormsControllerUnhandledException;
            WindowsFormsController.DefaultControllerFactory = controllerFactory;

            Application.ThreadException += OnApplicationThreadException;
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm = new MainForm();
        }

        public void Run()
        {
            Application.Run(this);
        }

        protected override void ExitThreadCore()
        {
            base.ExitThreadCore();

            lifetime.StopApplication();
        }

        private static void HandleUnhandledException(Exception exception) => MessageBox.Show(exception.ToString());

        private static void OnWindowsFormsControllerUnhandledException(object sender, Charites.Windows.Mvc.UnhandledExceptionEventArgs e)
        {
            HandleUnhandledException(e.Exception);
            e.Handled = true;
        }

        private static void OnApplicationThreadException(object sender, ThreadExceptionEventArgs e)
            => HandleUnhandledException(e.Exception);
    }
}
