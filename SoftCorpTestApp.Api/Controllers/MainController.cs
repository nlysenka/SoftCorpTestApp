using Microsoft.AspNetCore.Mvc;
using SoftCorpTestApp.Core.Interfaces.Infrastructure;

namespace SoftCorpTestApp.Api.Controllers
{
    [Route("api/main")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly ICoingeckoIntegration coingeckoIntegrationService;
        public MainController(ICoingeckoIntegration coingeckoIntegrationService)
        {
            this.coingeckoIntegrationService = coingeckoIntegrationService;
        }

        [HttpGet("trending")]
        public async Task<int> GetTreding()
        {
            var trending = await coingeckoIntegrationService.GetTrendingAsync();

            return 888;
        }
    }
}
