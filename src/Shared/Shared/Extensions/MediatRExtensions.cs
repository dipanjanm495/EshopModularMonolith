using Microsoft.Extensions.DependencyInjection;
using Shared.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatRWithAssemblies(this IServiceCollection services,params Assembly[] assemblies)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assemblies);
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });
            return services;
        }

    }
}
