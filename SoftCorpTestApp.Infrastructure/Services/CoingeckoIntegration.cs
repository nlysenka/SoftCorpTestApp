
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftCorpTestApp.Core.Configuration;
using SoftCorpTestApp.Core.DTO;
using SoftCorpTestApp.Core.Interfaces.Infrastructure;

namespace SoftCorpTestApp.Infrastructure.Services
{
    public class CoingeckoIntegration : ICoingeckoIntegration
    {
        private readonly HttpClient _httpClient;
        private readonly CoingeckoConfiguration _configuration;

        public CoingeckoIntegration(HttpClient httpClient, CoingeckoConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<Coin>> GetCoinsAsync()
        {
            var response = await _httpClient.GetAsync($"{_configuration.BaseUrl}/coins/list");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var coins = JsonConvert.DeserializeObject<List<Coin>>(responseBody);

            return coins!;
        }

        public async Task<Dictionary<string, BaseCurrencies>> GetPricesAsync(List<string> listOfCoins)
        {
            var coinsParam = string.Join(",", listOfCoins);

            var queryParameters = new Dictionary<string, string>
            {
                { "ids", coinsParam },
                { "vs_currencies", "usd,eur,rub"}
            };

            var dictFormUrlEncoded = new FormUrlEncodedContent(queryParameters);
            var queryString = await dictFormUrlEncoded.ReadAsStringAsync();

            var response = await _httpClient.GetAsync($"{_configuration.BaseUrl}/simple/price?{queryString}");

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
           
            var jObject = JObject.Parse(responseBody);
           
            var values = jObject.ToObject<Dictionary<string, BaseCurrencies>>();
           
            return values!;
        }
    }
}
