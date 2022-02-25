using Charites.Windows.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$;

public class $safeprojectname$ControllerFactory : IWindowsFormsControllerFactory
{
    private readonly IServiceProvider services;

    public $safeprojectname$ControllerFactory(IServiceProvider services)
    {
        this.services = services;
    }

    public object Create(Type controllerType) => services.GetRequiredService(controllerType);
}