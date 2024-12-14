using Proxy;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// TODO: возможность настройки GrpcClientFactory

// TODO: мониторинг запросов клиента

builder.Services
    .AddApplications()
    .AddInfrastructure(configuration)
    .AddPresenter();

var app = builder.Build();


app
    .UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStorage.Proxy v1");
        c.RoutePrefix = "swagger";
    })
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

app.Run();
