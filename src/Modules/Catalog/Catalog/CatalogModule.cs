using Catalog.Data;
using Catalog.Data.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;
using Shared.Data.Seed;

namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services,IConfiguration configuration)
        {
            //services.AddScoped<IProductRepository, ProductRepository>();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            services.AddDbContext<CatalogDbContext>(options =>
                options.UseNpgsql(connectionString));
            services.AddScoped<IDataSeeder, CatalogDataSeeder>();
            return services;
        }

        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
        {
            app.UseMigration<CatalogDbContext>();
            return app;
        }
    }
}
