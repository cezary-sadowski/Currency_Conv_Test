using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Currency_Conv_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyConverterController : ControllerBase
    {
        private readonly DBConnection _dbConnection;

        public CurrencyConverterController(DBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpGet]
        public ContentResult Get()
        {
            var request = new Requests()
            {
                Date = DateTime.Now,
                Name = nameof(CurrenciesService.GetListOfCurrenciesWithCode)
            };
            _dbConnection.Requests.Add(request);
            _dbConnection.SaveChanges();

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

            var request = new Requests()
            {
                Date = DateTime.Now,
                Name = nameof(CurrenciesService.GetCurrentRateForCurrency)
            };
            _dbConnection.Requests.Add(request);
            _dbConnection.SaveChanges();

            return new ContentResult
            {
                ContentType = "text/html; charset=utf-8",
                Content = $"Current rate for currency <b>{ code.ToUpper() }</b> is <b>{ result }</b>"
            };
        }

        [HttpGet]
        [Route("{value}/{fromCode}/{toCode}")]
        public ContentResult CalculateRates(decimal value, string fromCode, string toCode)
        {
            var result = CurrenciesService.CalculateRateBetweenCurrencies(value, fromCode.ToUpper(), toCode.ToUpper());

            var request = new Requests()
            {
                Date = DateTime.Now,
                Name = nameof(CurrenciesService.CalculateRateBetweenCurrencies)
            };
            _dbConnection.Requests.Add(request);
            _dbConnection.SaveChanges();

            return new ContentResult
            {
                ContentType = "text/html; charset=utf-8",
                Content = $"{value} <b>{fromCode}</b> is {result} <b>{toCode}</b>"
            };
        }
    }
}
