// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading;
using System.Windows.Forms;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;

namespace Charites.Windows.Samples.SimpleLoginDemo
{
    internal static class SimpleLoginDemo
    {
        [STAThread]
        private static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += OnAppDomainUnhandledException;
            Application.ThreadException += OnApplicationThreadException;

            WindowsFormsController.DefaultControllerFactory = new SimpleLoginDemoControllerFactory();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(new ApplicationHost(new LoginContent())));
        }

        private static void HandledUnhandledException(Exception exception) => MessageBox.Show(exception.ToString());

        private static void OnAppDomainUnhandledException(object sender, System.UnhandledExceptionEventArgs e)
            => HandledUnhandledException(e.ExceptionObject as Exception);

        private static void OnApplicationThreadException(object sender, ThreadExceptionEventArgs e)
            => HandledUnhandledException(e.Exception);
    }
}
