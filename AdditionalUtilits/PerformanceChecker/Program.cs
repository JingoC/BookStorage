using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PerformanceChecker.RpsChecker;

var serviceProvider = new ServiceCollection()
    .AddLogging(x =>
    {
        x.AddConsole();
        x.AddFilter("Refit", LogLevel.None);
        x.AddFilter("System.Net.Http.HttpClient", LogLevel.None);
    })
    .AddRpsChecker()
    .BuildServiceProvider();

var logger = serviceProvider.GetRequiredService<ILogger<RpsCore>>();

var rpsResults = await serviceProvider.GetRequiredService<RpsCore>().ExecuteAsync(CancellationToken.None);

foreach (var rpsResult in rpsResults)
{
    logger.LogInformation($"{rpsResult.Name}: {rpsResult.Rps} rps");
}