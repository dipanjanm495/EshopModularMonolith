var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCatalogModule(builder.Configuration).
                AddOrderingModule(builder.Configuration).
                AddBasketModule(builder.Configuration);

var app = builder.Build();

app.UseCatalogModule().UseOrderingModule().UseBasketModule();

//app.UseStaticFiles();
//app.UseRouting();   
//app.UseAuthentication();
//app.UseAuthorization();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

app.Run();
