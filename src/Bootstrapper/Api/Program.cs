using Carter;
using Serilog;
using Shared.Exceptions.Handler;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarterWithAssemblies(typeof(CatalogModule).Assembly);
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCatalogModule(builder.Configuration).
                AddOrderingModule(builder.Configuration).
                AddBasketModule(builder.Configuration);

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseExceptionHandler();

app.UseCatalogModule().UseOrderingModule().UseBasketModule();

app.MapCarter();

app.Run();
