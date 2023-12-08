using Charites.Windows.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WindowsFormsMvcApp;

public class WindowsFormsMvcAppControllerFactory(IServiceProvider services) : IWindowsFormsControllerFactory
{
    public object Create(Type controllerType) => services.GetRequiredService(controllerType);
}