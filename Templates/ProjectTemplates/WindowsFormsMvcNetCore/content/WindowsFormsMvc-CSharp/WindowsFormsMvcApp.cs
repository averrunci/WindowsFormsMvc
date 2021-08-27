using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace WindowsFormsMvcApp
{
    internal class WindowsFormsMvcApp : IHostedService
    {
        private readonly IWindowsFormsMvcAppApplication application;

        public WindowsFormsMvcApp(IWindowsFormsMvcAppApplication application)
        {
            this.application = application ?? throw new ArgumentNullException(nameof(application));
        }

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
}
