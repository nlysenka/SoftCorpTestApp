
using SoftCorpTestApp.Core.Interfaces.Infrastructure;

namespace SoftCorpTestApp.Infrastructure.Services
{
    public class CoingeckoIntegration : ICoingeckoIntegration
    {
        private readonly HttpClient _httpClient;

        public CoingeckoIntegration(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<string> GetTrendingAsync()
        {
            var response = await _httpClient.GetAsync("https://api.coingecko.com/api/v3/search/trending");
            
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
    }
}
