using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Shared.Messaging.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransitWithAssemblies(this IServiceCollection services,IConfiguration configuration, params Assembly[] assemblies)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.SetKebabCaseEndpointNameFormatter();
                cfg.SetInMemorySagaRepositoryProvider();
                cfg.AddConsumers(assemblies);
                cfg.AddSagaStateMachines(assemblies);
                cfg.AddSagas(assemblies);
                cfg.AddActivities(assemblies);
                cfg.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(configuration["MessageBroker:Host"]!), h =>
                    {
                        h.Username(configuration["MessageBroker:UserName"]!);
                        h.Password(configuration["MessageBroker:Password"]!);
                    });

                    cfg.ConfigureEndpoints(context);
                });

                //cfg.UsingInMemory((context, config) =>
                //{
                //    config.ConfigureEndpoints(context);
                //});
            });
            return services;
        }
    }
}
