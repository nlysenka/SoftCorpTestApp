using Microsoft.AspNetCore.Mvc;
using SoftCorpTestApp.Core.Interfaces.Infrastructure;

namespace SoftCorpTestApp.Api.Controllers
{
    [Route("api/main")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly ICoingeckoIntegration _coingeckoIntegrationService;
        public MainController(ICoingeckoIntegration coingeckoIntegrationService)
        {
            _coingeckoIntegrationService = coingeckoIntegrationService;
        }

        [HttpGet("trending")]
        public async Task<string> GetTreding()
        {
            var trending = await _coingeckoIntegrationService.GetTrendingAsync();

            return trending;
        }
    }
}
