using SoftCorpTestApp.Core.DTO;
using SoftCorpTestApp.Core.Interfaces.Services;

namespace SoftCorpTestApp.Core.Services
{
    public class WorkerControl : IWorkerControl
    {
        private Dictionary<string, Dictionary<string, decimal>> _cryptoCurrencyPrices = new();

        public Dictionary<string, Dictionary<string, decimal>> GetCryptoCurrencyPrices()
        {
            return _cryptoCurrencyPrices;
        }

        public void SetCryptoCurrencyPrices(Dictionary<string, Dictionary<string, decimal>> result)
        {
            _cryptoCurrencyPrices.Clear();
            _cryptoCurrencyPrices = result;
        }
    }
}
