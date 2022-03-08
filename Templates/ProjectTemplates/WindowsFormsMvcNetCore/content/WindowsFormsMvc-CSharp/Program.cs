using Charites.Windows.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WindowsFormsMvcApp;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        CreateHostBuilder().Build().Run();
    }

    private static IHostBuilder CreateHostBuilder()
        => Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => ConfigureServices(context.Configuration, services));

    private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        => services.AddHostedService<WindowsFormsMvcApp>()
            .AddSingleton<IWindowsFormsControllerFactory, WindowsFormsMvcAppControllerFactory>()
            .AddSingleton<IWindowsFormsMvcAppApplication, WindowsFormsMvcAppApplication>()
            .AddControllers();
}