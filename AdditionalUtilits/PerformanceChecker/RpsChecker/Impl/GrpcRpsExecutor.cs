using BookStorage;
using Grpc.Net.ClientFactory;
using PerformanceChecker.RpsChecker.Abstract;
using PerformanceChecker.RpsChecker.Models;

namespace PerformanceChecker.RpsChecker.Impl
{
    public class GrpcRpsExecutor : IRpsExecutor
    {
        private readonly GrpcClientFactory _grpcClientFactory;

        public GrpcRpsExecutor(GrpcClientFactory grpcClientFactory)
        {
            _grpcClientFactory = grpcClientFactory;
        }

        public void Dispose()
        {
        }

        public async Task<RpsResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var countClients = 100;

            var clients = new List<BookService.BookServiceClient>();

            for (int i = 0; i < countClients; i++)
            {
                clients.Add(_grpcClientFactory.CreateClient<BookService.BookServiceClient>("Book"));
            }

            var startDtn = DateTime.Now;

            var tasks = clients.Select(SendRequestAsync).ToArray();

            await Task.WhenAll(tasks);

            var stopDtn = DateTime.Now;

            return new RpsResult()
            {
                Name = "BookStorage.Grpc",
                Rps = (int) ((double) countClients / (stopDtn - startDtn).TotalSeconds)
            };
        }

        private static Task<GetAllGrpcResponse> SendRequestAsync(BookService.BookServiceClient client)
        {
            return client.GetAllAsync(new GetAllGrpcRequest()).ResponseAsync;
        }
    }
}
