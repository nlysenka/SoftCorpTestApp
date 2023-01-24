﻿using Microsoft.Extensions.Hosting;
using SoftCorpTestApp.Core.Configuration;
using SoftCorpTestApp.Core.Interfaces.Infrastructure;
using SoftCorpTestApp.Core.Interfaces.Services;

namespace SoftCorpTestApp.Core.Services
{
    public interface IMonitorWorker
    {
        void TestMethod();
    }
    public class MonitorWorker : BackgroundService
    {
        private readonly ICoinGeckoIntegration _coinGeckoIntegration;
        private readonly IWorkerControl _workerControl;
        private readonly CoinGeckoConfiguration _coinGeckoConfiguration;

        public MonitorWorker(ICoinGeckoIntegration coinGeckoIntegration, IWorkerControl workerControl, CoinGeckoConfiguration coinGeckoConfiguration)
        {
            _coinGeckoIntegration = coinGeckoIntegration;
            _workerControl = workerControl;
            _coinGeckoConfiguration = coinGeckoConfiguration;
            Console.WriteLine("MonitorWorker is initialized!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"Worker running at: {DateTimeOffset.Now}");

                var coins = await _coinGeckoIntegration.GetCoinsAsync();

                var prices = await _coinGeckoIntegration.GetPricesAsync(coins.Take(100).Select(p => p.Id).ToList(), _coinGeckoConfiguration.BaseCurrencies);

                _workerControl.SetCryptoCurrencyPrices(prices);

                await Task.Delay(30000, stoppingToken);
            }
        }

        public void TestMethod()
        {
            Console.WriteLine("1");
        }
    }
}
