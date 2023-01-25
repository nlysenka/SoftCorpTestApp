using SoftCorpTestApp.Core.DTO;

namespace SoftCorpTestApp.Core.Interfaces.Services
{
    public interface IWorkerControl
    {
        void SetCryptoCurrencyPrices(Dictionary<string, Dictionary<string, decimal>> result);

        Dictionary<string, Dictionary<string, decimal>> GetCryptoCurrencyPrices();

        void SetExchangeRates(List<ExchangeRate> exchangeRates);

        decimal GetConvertedValue(decimal sum, string coin, string currency);
    }
}
