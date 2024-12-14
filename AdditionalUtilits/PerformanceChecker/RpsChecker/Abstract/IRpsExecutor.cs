using PerformanceChecker.RpsChecker.Models;

namespace PerformanceChecker.RpsChecker.Abstract
{
    public interface IRpsExecutor : IDisposable
    {
        Task<RpsResult> ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
