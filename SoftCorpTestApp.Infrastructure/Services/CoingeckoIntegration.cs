
using SoftCorpTestApp.Core.Configuration;
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

        public async Task<string> GetTrendingAsync()
        {
            var response = await _httpClient.GetAsync($"{_configuration.BaseUrl}/search/trending");
            
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
           
            return responseBody;
        }
    }
}
