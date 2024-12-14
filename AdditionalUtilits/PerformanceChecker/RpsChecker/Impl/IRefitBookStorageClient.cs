using Refit;
using RestApi.Contracts.Book.GetAll;

namespace PerformanceChecker.RpsChecker.Impl
{
    public interface IRefitBookStorageClient
    {
        [Get("/book/all")]
        Task<GetAllHttpResponse> GetAll(CancellationToken token = default);
    }
}
