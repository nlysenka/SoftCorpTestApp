using SoftCorpTestApp.Core.DTO;

namespace SoftCorpTestApp.Core.Interfaces.Infrastructure
{
    public interface ICoingeckoIntegration
    {
        Task<List<Coin>> GetCoinsAsync();

        Task<Dictionary<string, BaseCurrencies>> GetPricesAsync(List<string> listOfCoins);
    }
}
