using System.Linq;
using System.Reflection;
using Charites.Windows.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WindowsFormsMvcApp
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddControllers(this IServiceCollection services)
            => Assembly.GetExecutingAssembly().DefinedTypes
                .Where(type => type.GetCustomAttributes<ViewAttribute>(true).Any())
                .Aggregate(services, (s, t) => s.AddTransient(t));
    }
}
