using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Book
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBookUseCases(this IServiceCollection services)
            => services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    }
}
