using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftCorpTestApp.Core.Configuration;
using SoftCorpTestApp.Core.DTO;
using SoftCorpTestApp.Core.Interfaces.Infrastructure;

namespace SoftCorpTestApp.Infrastructure.Services
{
    public class CoinGeckoIntegration : ICoinGeckoIntegration
    {
        private readonly HttpClient _httpClient;

        public CoinGeckoIntegration(IHttpClientFactory httpClientFactory, CoinGeckoConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(configuration.BaseUrl);
        }

        public async Task<List<Coin>> GetCoinsAsync()
        {
            var response = await _httpClient.GetAsync("coins/list");

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var coins = JsonConvert.DeserializeObject<List<Coin>>(responseBody);

            return coins!;
        }

        public async Task<Dictionary<string, Dictionary<string, decimal>>> GetPricesAsync(List<string> listOfCoins, List<string> listOfCurrencies)
        {
            var coinsParam = string.Join(",", listOfCoins);
            var currenciesParam = string.Join(",", listOfCurrencies);

            var queryParameters = new Dictionary<string, string>
            {
                { "ids", coinsParam },
                { "vs_currencies", currenciesParam}
            };

            var dictFormUrlEncoded = new FormUrlEncodedContent(queryParameters);
            var queryString = await dictFormUrlEncoded.ReadAsStringAsync();

            var response = await _httpClient.GetAsync($"simple/price?{queryString}");

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
           
            var jObject = JObject.Parse(responseBody);
           
            var values = jObject.ToObject<Dictionary<string, Dictionary<string,decimal>>>();
           
            return values!;
        }

        public async Task<List<ExchangeRate>> GetExchangeRatesAsync()
        {
            var response = await _httpClient.GetAsync("exchange_rates");

            response.EnsureSuccessStatusCode();
            
            var responseBody = await response.Content.ReadAsStringAsync();

            var resultWrapper = JsonConvert.DeserializeObject<Dictionary<string,Dictionary<string, CoinValue>>>(responseBody)!.FirstOrDefault();

            var result = resultWrapper.Value.ToList().Select(keyValuePair => new ExchangeRate {NameCurrency = keyValuePair.Key, Value = keyValuePair.Value.Value}).ToList();

            return result;
        }
    }
}