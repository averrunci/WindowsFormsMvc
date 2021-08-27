using System;
using Charites.Windows.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WindowsFormsMvcApp
{
    public class WindowsFormsMvcAppControllerFactory : IWindowsFormsControllerFactory
    {
        private readonly IServiceProvider services;

        public WindowsFormsMvcAppControllerFactory(IServiceProvider services)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public object Create(Type controllerType)
        {
            return services.GetRequiredService(controllerType);
        }
    }
}
