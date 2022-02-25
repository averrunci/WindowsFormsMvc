using Charites.Windows.Mvc;
using Microsoft.Extensions.Hosting;

namespace $safeprojectname$;

internal class $safeprojectname$Application : ApplicationContext, I$safeprojectname$Application
{
    private readonly IHostApplicationLifetime lifetime;

    public $safeprojectname$Application(IHostApplicationLifetime lifetime, IWindowsFormsControllerFactory controllerFactory)
    {
        this.lifetime = lifetime;

        WindowsFormsController.UnhandledException += OnWindowsFormsControllerUnhandledException;
        WindowsFormsController.DefaultControllerFactory = controllerFactory;

        Application.ThreadException += OnApplicationThreadException;
        ApplicationConfiguration.Initialize();

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

    private static void OnWindowsFormsControllerUnhandledException(object? sender, Charites.Windows.Mvc.UnhandledExceptionEventArgs e)
    {
        HandleUnhandledException(e.Exception);
        e.Handled = true;
    }

    private static void OnApplicationThreadException(object? sender, ThreadExceptionEventArgs e)
        => HandleUnhandledException(e.Exception);
}