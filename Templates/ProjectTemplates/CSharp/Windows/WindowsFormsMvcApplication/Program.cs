using Charites.Windows.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace $safeprojectname$;

internal static class Program
{
    [STAThread]
    private static async Task Main()
    {
        await CreateHostBuilder().Build().RunAsync();
    }
    
    private static IHostBuilder CreateHostBuilder()
        => Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => ConfigureServices(context.Configuration, services));
    
    private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        => services.AddHostedService<$safeprojectname$>()
            .AddSingleton<IWindowsFormsControllerFactory, $safeprojectname$ControllerFactory>()
            .AddSingleton<I$safeprojectname$Application, $safeprojectname$Application>()
            .AddControllers();
}