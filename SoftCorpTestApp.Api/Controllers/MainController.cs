using Microsoft.AspNetCore.Mvc;
using SoftCorpTestApp.Core.DTO;
using SoftCorpTestApp.Core.Interfaces.Services;

namespace SoftCorpTestApp.Api.Controllers
{
    [Route("api/main")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IWorkerControl _workerControl;
        public MainController(IWorkerControl workerControl)
        {
            _workerControl = workerControl;
        }

        [HttpGet("coins")]
        public Dictionary<string, BaseCurrencies> GetCoins()
        {
            var result = _workerControl.GetCryptoCurrencyPrices();
            return result;
        }
    }
}
