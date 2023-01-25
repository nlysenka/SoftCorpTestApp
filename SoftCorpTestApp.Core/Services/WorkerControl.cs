using SoftCorpTestApp.Core.DTO;
using SoftCorpTestApp.Core.Interfaces.Services;

namespace SoftCorpTestApp.Core.Services
{
    public class WorkerControl : IWorkerControl
    {
        private Dictionary<string, Dictionary<string, decimal>> _cryptoCurrencyPrices = new();
        private List<ExchangeRate> _exchangeRates = new();

        public Dictionary<string, Dictionary<string, decimal>> GetCryptoCurrencyPrices()
        {
            return _cryptoCurrencyPrices;
        }

        public void SetCryptoCurrencyPrices(Dictionary<string, Dictionary<string, decimal>> result)
        {
            _cryptoCurrencyPrices.Clear();
            _cryptoCurrencyPrices = result;
        }

        public void SetExchangeRates(List<ExchangeRate> exchangeRates)
        {
            _exchangeRates.Clear();
            _exchangeRates = exchangeRates;
        }

        public decimal GetConvertedValue(decimal sum, string coin, string currency)
        {
            var currencyValue = _exchangeRates.FirstOrDefault(p => p.NameCurrency == currency)!.Value;
            var coinValue = _exchangeRates.FirstOrDefault(p => p.NameCurrency == coin)!.Value;

            var result = currencyValue / coinValue * sum;
            return result;
        }
    }
}
