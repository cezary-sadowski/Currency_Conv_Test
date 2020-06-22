using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Currency_Conv_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyConverterController : ControllerBase
    {
        private readonly ILogger<CurrencyConverterController> _logger;

        public CurrencyConverterController(ILogger<CurrencyConverterController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ContentResult Get()
        {
            return new ContentResult
            {
                ContentType = "text/html; charset=utf-8",
                Content = CurrenciesService.GetListOfCurrenciesWithCode()
            };
        }

        [HttpGet]
        [Route("{code}")]
        public ContentResult GetCurrentRateForCode(string code)
        {
            var result = CurrenciesService.GetCurrentRateForCurrency(code.ToUpper());

            return new ContentResult
            {
                ContentType = "text/html; charset=utf-8",
                Content = $"Current rate for currency <b>{code.ToUpper()}</b> is <b>{result}</b>"
            };
        }

        [HttpGet]
        [Route("{value}/{fromCode}/{toCode}")]
        public ContentResult CalculateRates(decimal value, string fromCode, string toCode)
        {
            var result = CurrenciesService.CalculateRateBetweenCurrencies(value, fromCode.ToUpper(), toCode.ToUpper());

            return new ContentResult
            {
                ContentType = "text/html; charset=utf-8",
                Content = $"{result}"
            };
        }


    }
}
