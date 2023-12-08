using Charites.Windows.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$;

public class $safeprojectname$ControllerFactory(IServiceProvider services) : IWindowsFormsControllerFactory
{
    public object Create(Type controllerType) => services.GetRequiredService(controllerType);
}