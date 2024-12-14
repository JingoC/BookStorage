using Microsoft.OpenApi.Models;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplications()
    .AddInfrastructure()
    .AddControllers().Services
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStorage.RestApi", Version = "v1" });
    });

var app = builder.Build();


app
    .UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStorage.RestApi v1");
        c.RoutePrefix = "swagger";
    })
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

app.Run();
