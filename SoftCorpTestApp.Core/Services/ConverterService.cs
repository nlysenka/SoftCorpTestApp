using SoftCorpTestApp.Core.Interfaces.Infrastructure;
using SoftCorpTestApp.Core.Interfaces.Services;

namespace SoftCorpTestApp.Core.Services
{
    public class ConverterService : IConverterService
    {
        private readonly ICoinGeckoIntegration _coinGeckoIntegration;

        public ConverterService(ICoinGeckoIntegration coinGeckoIntegration)
        {
            _coinGeckoIntegration = coinGeckoIntegration;
        }

        public async Task<decimal> ConvertCoinToCurrency(decimal sum, string coin, string currency)
        {
            var coins = new List<string>
            {
                coin
            };

            var currencies = new List<string>
            {
                currency
            };

            var supportedCurrencies = await _coinGeckoIntegration.GetSupportedCurrencies();

            if (!supportedCurrencies.Contains(currency))
            {
                throw new Exception($@"Currency {currency} is not supported to convert.");
            }

            var price = await _coinGeckoIntegration.GetPricesAsync(coins, currencies);

            var priceInSelectedCurrency = price.FirstOrDefault().Value.FirstOrDefault().Value;

            return sum * priceInSelectedCurrency;
        }
    }
}
