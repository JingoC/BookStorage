using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.BookSearch
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBookSearchUseCases(this IServiceCollection services)
            => services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    }
}
