using SoftCorpTestApp.Core.DTO;
using SoftCorpTestApp.Core.Interfaces.Services;

namespace SoftCorpTestApp.Core.Services
{
    public class WorkerControl : IWorkerControl
    {
        private Dictionary<string, BaseCurrencies> _cryptoCurrencyPrices = new();

        public Dictionary<string, BaseCurrencies> GetCryptoCurrencyPrices()
        {
            return _cryptoCurrencyPrices;
        }

        public void SetCryptoCurrencyPrices(Dictionary<string, BaseCurrencies> result)
        {
            _cryptoCurrencyPrices.Clear();
            _cryptoCurrencyPrices = result;
        }
    }
}
