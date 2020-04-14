// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading;
using System.Windows.Forms;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;
using Microsoft.Extensions.Hosting;

namespace Charites.Windows.Samples.SimpleLoginDemo
{
    internal class SimpleLoginDemoApplication : ApplicationContext, ISimpleLoginDemoApplication
    {
        private readonly IHostApplicationLifetime lifetime;

        public SimpleLoginDemoApplication(IHostApplicationLifetime lifetime, IWindowsFormsControllerFactory controllerFactory)
        {
            this.lifetime = lifetime;

            WindowsFormsController.UnhandledException += OnWindowsFormsControllerUnhandledException;
            WindowsFormsController.DefaultControllerFactory = controllerFactory;

            Application.ThreadException += OnApplicationThreadException;
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm = new MainForm(new ApplicationHost(new LoginContent()));
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

        private static void OnWindowsFormsControllerUnhandledException(object sender, Mvc.UnhandledExceptionEventArgs e)
        {
            HandleUnhandledException(e.Exception);
            e.Handled = true;
        }

        private static void OnApplicationThreadException(object sender, ThreadExceptionEventArgs e)
            => HandleUnhandledException(e.Exception);
    }
}
