using Microsoft.Extensions.DependencyInjection;
using PerformanceChecker.RpsChecker.Abstract;
using PerformanceChecker.RpsChecker.Models;
using RestApi.Contracts.Book.GetAll;

namespace PerformanceChecker.RpsChecker.Impl
{
    public class RestApiRpsExecutor : IRpsExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        public RestApiRpsExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
        }

        public async Task<RpsResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var countClients = 100;

            var clients = new List<IRefitBookStorageClient>();

            for (int i = 0; i < countClients; i++)
            {
                clients.Add(_serviceProvider.GetRequiredService<IRefitBookStorageClient>());
            }

            var startDtn = DateTime.Now;

            var tasks = clients.Select(SendRequestAsync).ToArray();

            await Task.WhenAll(tasks);

            var stopDtn = DateTime.Now;

            return new RpsResult()
            {
                Name = "BookStorage.RestApi",
                Rps = (int) ((double) countClients / (stopDtn - startDtn).TotalSeconds)
            };
        }

        private static Task<GetAllHttpResponse> SendRequestAsync(IRefitBookStorageClient client)
        {
            return client.GetAll(CancellationToken.None);
        }
    }
}
