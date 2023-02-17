using System;
using System.Net;
using SoftCorpTestApp.Core.DTO;
using SoftCorpTestApp.Core.Exceptions;
using SoftCorpTestApp.Core.Interfaces.Services;
using static SoftCorpTestApp.Core.Exceptions.SoftCorpExceptionResponse;

namespace SoftCorpTestApp.Core.Services
{
    public class WorkerControl : IWorkerControl
    {
        private Dictionary<string, Dictionary<string, decimal>> _cryptoCurrencyPrices = new();
        private List<ExchangeRate> _exchangeRates = new();

        public Dictionary<string, Dictionary<string, decimal>> GetCryptoCurrencyPrices()
        {
            return _cryptoCurrencyPrices;
        }

        public void SetCryptoCurrencyPrices(Dictionary<string, Dictionary<string, decimal>> cryptoCurrencyPrices)
        {
            _cryptoCurrencyPrices.Clear();
            _cryptoCurrencyPrices = cryptoCurrencyPrices;
        }

        public void SetExchangeRates(List<ExchangeRate> exchangeRates)
        {
            _exchangeRates.Clear();
            _exchangeRates = exchangeRates;
        }

        public decimal GetConvertedValue(decimal sum, string coin, string currency)
        {
            
            var currencyValue = _exchangeRates.FirstOrDefault(p => p.NameCurrency == currency)!;
            var coinValue = _exchangeRates.FirstOrDefault(p => p.NameCurrency == coin);

            if (currencyValue == null || coinValue == null)
            {
                throw new SoftCorpException(HttpStatusCode.BadRequest, $"The coin '{coin}' or currency '{currency}' is not supported for conversion."); 
            }

            var result = currencyValue.Value / coinValue.Value * sum;
            return result;
        }
    }
}
