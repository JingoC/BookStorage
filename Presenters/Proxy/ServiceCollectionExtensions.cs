using Application.Book;
using Application.BookSearch;
using BookStorage;
using Database;
using Grpc.Client.DependencyInjection;
using Grpc.Client.DependencyInjection.Interceptors;
using Microsoft.OpenApi.Models;
using Proxy.Configurations;

namespace Proxy
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
            => services
                .AddBookUseCases()
                .AddBookSearchUseCases();

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
            => services.AddDatabase()
                .AddGrpcClient(configuration);

        public static IServiceCollection AddPresenter(this IServiceCollection services)
            => services
                .AddControllers().Services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo {Title = "BookStorage.Proxy", Version = "v1"});
                });

        private static IServiceCollection AddGrpcClient(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddScoped<LoggingInterceptor>()
                .RegisterGrpcClient<BookService.BookServiceClient, BookGrpcClientConfiguration>("book", configuration)
                .Services
                .RegisterGrpcClient<BookSearchService.BookSearchServiceClient, BookSearchGrpcClientConfiguration>("booksearch", configuration)
                .Services;
    }
}
