using Application.Book;
using Application.BookSearch;
using Database;

namespace WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
            => services
                .AddBookUseCases()
                .AddBookSearchUseCases();

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
            => services.AddDatabase();
    }
}
