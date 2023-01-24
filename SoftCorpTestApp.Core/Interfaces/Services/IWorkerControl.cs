using SoftCorpTestApp.Core.DTO;

namespace SoftCorpTestApp.Core.Interfaces.Services
{
    public interface IWorkerControl
    {
        void SetCryptoCurrencyPrices(Dictionary<string, BaseCurrencies> result);
        Dictionary<string, BaseCurrencies> GetCryptoCurrencyPrices();
    }
}
