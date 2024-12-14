using Application.Book;
using Application.BookSearch;
using Database;
using Grpc.Interceptors;
using Microsoft.OpenApi.Models;

namespace Grpc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
            => services
                .AddBookUseCases()
                .AddBookSearchUseCases();

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
            => services.AddDatabase();

        public static IServiceCollection AddPresenter(this IServiceCollection services)
        => services
            .AddGrpcSwagger()
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStorage.Grpc", Version = "v1" });
            })
            .AddGrpc(x =>
            {
                x.Interceptors.Add<ExceptionHandlerInterceptor>();
            })
            .Services;
    }
}
