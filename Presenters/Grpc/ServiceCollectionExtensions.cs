using Application.Book;
using Application.BookSearch;
using Database;
using Grpc.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("test_test_test_test_test_test_test_test_test_test"))
                };
            }).Services
            .AddAuthorization()
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
