using Database.Interface;
using Database.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
            => services.AddSingleton<IBookProvider, BookProvider>();
    }
}
