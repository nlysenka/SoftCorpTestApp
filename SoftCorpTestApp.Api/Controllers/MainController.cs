using Microsoft.AspNetCore.Mvc;
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
        public Dictionary<string, Dictionary<string, decimal>> GetCoinsWithBaseCurrencies()
        {
            var result = _workerControl.GetCryptoCurrencyPrices();
            return result;
        }

        /// <summary>
        /// Supported coin/currrencies in readme file.
        /// </summary>
        [HttpGet("convert")]
        public decimal ConvertCoinToCurrency(
            [FromHeader] decimal sum = 1,
            [FromHeader] string coin = "usd",
            [FromHeader] string currency = "btc"
        )
        {
            var result = _workerControl.GetConvertedValue(sum, coin, currency);
            return result;
        }
    }
}
