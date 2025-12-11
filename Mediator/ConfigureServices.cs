using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public static class ConfigureServices
    {
        public static void AddMediator(this IServiceCollection services) {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        }
    }
}
