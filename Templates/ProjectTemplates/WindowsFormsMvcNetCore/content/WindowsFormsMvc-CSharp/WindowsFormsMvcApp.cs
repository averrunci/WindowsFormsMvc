using Microsoft.Extensions.Hosting;

namespace WindowsFormsMvcApp;

internal class WindowsFormsMvcApp(IWindowsFormsMvcAppApplication application) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        application.Run();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}