using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Token
{
    public static class ConfigureServices
    {
        public static void AddTokenService(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
