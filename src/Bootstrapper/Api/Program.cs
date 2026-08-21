using Carter;
using FluentValidation;
using Serilog;
using Shared.Behaviours;
using Shared.Exceptions.Handler;
using Shared.Extensions;
using Shared.Messaging.Extensions;

var builder = WebApplication.CreateBuilder(args);

var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;

builder.Services.AddCarterWithAssemblies(catalogAssembly, basketAssembly);

builder.Services.AddMediatRWithAssemblies(catalogAssembly, basketAssembly);

builder.Services.AddValidatorsFromAssemblies([basketAssembly,catalogAssembly]);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddMassTransitWithAssemblies(builder.Configuration,catalogAssembly, basketAssembly);

builder.Services.AddCatalogModule(builder.Configuration).
                AddOrderingModule(builder.Configuration).
                AddBasketModule(builder.Configuration);

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseExceptionHandler();

app.UseCatalogModule().UseOrderingModule().UseBasketModule();

app.MapCarter();

app.Run();
