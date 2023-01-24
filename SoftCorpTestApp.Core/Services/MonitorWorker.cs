using Microsoft.Extensions.Hosting;
using SoftCorpTestApp.Core.Interfaces.Infrastructure;
using SoftCorpTestApp.Core.Interfaces.Services;

namespace SoftCorpTestApp.Core.Services
{
    public class MonitorWorker : BackgroundService
    {
        private readonly ICoingeckoIntegration _coingeckoIntegration;
        private readonly IWorkerControl _workerControl;

        public MonitorWorker(ICoingeckoIntegration coingeckoIntegration, IWorkerControl workerControl)
        {
            _coingeckoIntegration = coingeckoIntegration;
            _workerControl = workerControl;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"Worker running at: {DateTimeOffset.Now}");

                var coins = await _coingeckoIntegration.GetCoinsAsync();

                var prices = await _coingeckoIntegration.GetPricesAsync(coins.Take(100).Select(p => p.Id).ToList());

                _workerControl.SetCryptoCurrencyPrices(prices);

                await Task.Delay(30000, stoppingToken);
            }
        }
    }
}
