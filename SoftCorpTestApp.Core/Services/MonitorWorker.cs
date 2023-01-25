using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoftCorpTestApp.Core.Configuration;
using SoftCorpTestApp.Core.Interfaces.Infrastructure;
using SoftCorpTestApp.Core.Interfaces.Services;

namespace SoftCorpTestApp.Core.Services
{
    public class MonitorWorker : BackgroundService
    {
        private readonly IWorkerControl _workerControl;
        private readonly CoinGeckoConfiguration _coinGeckoConfiguration;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MonitorWorker(IWorkerControl workerControl,
            CoinGeckoConfiguration coinGeckoConfiguration, IServiceScopeFactory serviceScopeFactory)
        {
            _workerControl = workerControl;
            _coinGeckoConfiguration = coinGeckoConfiguration;
            _serviceScopeFactory = serviceScopeFactory;
            Console.WriteLine("MonitorWorker is initialized.");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var coinGeckoIntegration = scope.ServiceProvider.GetService<ICoinGeckoIntegration>();

                Console.WriteLine($"Worker running at: {DateTimeOffset.Now}");

                var coins = await coinGeckoIntegration?.GetCoinsAsync()!;

                var prices = 
                    await coinGeckoIntegration.GetPricesAsync(coins.Take(100).Select(p => p.Id).ToList(), _coinGeckoConfiguration.BaseCurrencies);

                var exchangeRates = await coinGeckoIntegration.GetExchangeRatesAsync();

                _workerControl.SetCryptoCurrencyPrices(prices);
                _workerControl.SetExchangeRates(exchangeRates);

                await Task.Delay(30000, stoppingToken);
            }
        }
    }
}
