using SoftCorpTestApp.Core.DTO;

namespace SoftCorpTestApp.Core.Interfaces.Infrastructure
{
    public interface ICoinGeckoIntegration
    {
        Task<List<Coin>> GetCoinsAsync();

        Task<Dictionary<string, Dictionary<string, decimal>>> GetPricesAsync(List<string> listOfCoins, List<string> listOfCurrencies);

        Task<List<string>> GetSupportedCurrencies();
    }
}
