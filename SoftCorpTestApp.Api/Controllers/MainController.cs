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

        [HttpGet("convert")]
        public decimal ConvertCoinToCurrency(
            [FromHeader] decimal sum,
            [FromHeader] string coin,
            [FromHeader] string currency
        )
        {
            var result = _workerControl.GetConvertedValue(sum, coin, currency);
            return result;
        }
    }
}
