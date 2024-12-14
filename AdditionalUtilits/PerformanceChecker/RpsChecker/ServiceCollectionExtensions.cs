using BookStorage;
using Microsoft.Extensions.DependencyInjection;
using PerformanceChecker.RpsChecker.Abstract;
using PerformanceChecker.RpsChecker.Impl;
using Refit;
using System.Text.Json;

namespace PerformanceChecker.RpsChecker
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRpsChecker(this IServiceCollection services)
            => services
                .AddGrpcClient<BookService.BookServiceClient>("Book", x =>
                {
                    x.Address = new Uri("https://localhost:6010");
                })
                .Services
                .AddRefitClient<IRefitBookStorageClient>(new RefitSettings()
                {
                    ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        PropertyNameCaseInsensitive = true
                    })
                })
                .ConfigureHttpClient((sp, c) =>
                {
                    c.BaseAddress = new Uri("https://localhost:6012/");
                })
                .Services
                .AddScoped<IRpsExecutor, GrpcRpsExecutor>()
                .AddScoped<IRpsExecutor, RestApiRpsExecutor>()
                .AddScoped<RpsCore>();
    }
}
