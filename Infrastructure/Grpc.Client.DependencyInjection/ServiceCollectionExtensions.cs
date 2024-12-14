using Grpc.Client.DependencyInjection.Configuration;
using Grpc.Client.DependencyInjection.Interceptors;
using Grpc.Net.Client;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Grpc.Client.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder RegisterGrpcClient<TClient, TConfiguration>(this IServiceCollection services,
            string name, IConfiguration configuration,
            Action<IServiceProvider, GrpcClientFactoryOptions>? configureOption = null,
            IReadOnlyCollection<Action<CallOptionsContext>>? callOptionsActions = null,
            IReadOnlyCollection<Action<GrpcChannelOptions>>? grpcOptionsActions = null
        )
            where TClient : class
            where TConfiguration : GrpcClientConfiguration
        {
            services.TryScoped<LoggingInterceptor>();
            services.Configure<TConfiguration>(configuration.GetSection(typeof(TConfiguration).Name.Replace("Configuration", string.Empty)));
            var builder = services.AddGrpcClient<TClient>(name, (sp, c) =>
                {
                    var config = sp.CreateScope().ServiceProvider
                        .GetRequiredService<IOptionsSnapshot<TConfiguration>>().Value;

                    c.Address = new Uri(config.Address);

                    configureOption?.Invoke(sp, c);

                    if (callOptionsActions != null)
                    {
                        foreach (var callOptionAction in callOptionsActions)
                        {
                            c.CallOptionsActions.Add(callOptionAction);
                        }
                    }

                    if (grpcOptionsActions != null)
                    {
                        foreach (var grpcOptionsAction in grpcOptionsActions)
                        {
                            c.ChannelOptionsActions.Add(grpcOptionsAction);
                        }
                    }
                })
                .AddInterceptor<LoggingInterceptor>();

            return builder;
        }

        private static IServiceCollection TryScoped<TService>(this IServiceCollection services)
            where TService : class
        {
            services.TryAddScoped<TService>();

            return services;
        }
    }
}
