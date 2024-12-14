using PerformanceChecker.RpsChecker.Abstract;
using PerformanceChecker.RpsChecker.Models;

namespace PerformanceChecker.RpsChecker
{
    public class RpsCore
    {
        private readonly IEnumerable<IRpsExecutor> _executor;

        public RpsCore(IEnumerable<IRpsExecutor> executor)
        {
            _executor = executor;
        }

        public async Task<IReadOnlyCollection<RpsResult>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var results = new List<RpsResult>();

            foreach (var rpsExecutor in _executor)
            {
                results.Add(await rpsExecutor.ExecuteAsync(cancellationToken));

                rpsExecutor.Dispose();
            }

            return results;
        }
    }
}
