using Grpc;
using Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

// TODO: добавить авторизация на grpc

// TODO: мониторинг запросов к серверу

// TODO: передача потоковых данных

builder.Services
    .AddApplications()
    .AddInfrastructure()
    .AddPresenter();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.MapGrpcService<BookGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
