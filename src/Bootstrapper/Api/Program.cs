using Carter;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarterWithAssemblies(typeof(CatalogModule).Assembly);

builder.Services.AddCatalogModule(builder.Configuration).
                AddOrderingModule(builder.Configuration).
                AddBasketModule(builder.Configuration);

var app = builder.Build();

app.UseCatalogModule().UseOrderingModule().UseBasketModule();

app.MapCarter();

app.Run();
