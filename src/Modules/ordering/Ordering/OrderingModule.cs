using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering
{
    public static class OrderingModule
    {

        public static IServiceCollection AddOrderingModule(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            //services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }

        public static IApplicationBuilder UseOrderingModule(this IApplicationBuilder app)
        {
            return app;
        }

    }
}
