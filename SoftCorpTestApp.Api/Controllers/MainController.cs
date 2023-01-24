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
        private readonly IConverterService _converterService;

        public MainController(IWorkerControl workerControl, IConverterService converterService)
        {
            _workerControl = workerControl;
            _converterService = converterService;
        }

        [HttpGet("coins")]
        public Dictionary<string, Dictionary<string, decimal>> GetCoinsWithBaseCurrencies()
        {
            var result = _workerControl.GetCryptoCurrencyPrices();
            return result;
        }

        [HttpGet("convert")]
        public async Task<decimal> ConvertCoinToCurrency(
            [FromHeader] decimal sum,
            [FromHeader] string coin,
            [FromHeader] string currency
        )
        {
            var result = await _converterService.ConvertCoinToCurrency(sum, coin, currency);
            return result;
        }
    }
}
