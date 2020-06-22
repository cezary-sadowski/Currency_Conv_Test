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

        static async Task<string> GetCurrencies()
        {
            var httpClient = HttpClientFactory.Create();

            var url = "http://api.nbp.pl/api/exchangerates/tables/a/";
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);

            if(httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var content = httpResponseMessage.Content;
                var data = await content.ReadAsStringAsync();
                return data;
            }

            return httpResponseMessage.StatusCode.ToString();
        }

        [HttpGet]
        public ContentResult Get()
        {
            var data = GetCurrencies().Result;
            var currencies = JsonConvert.DeserializeObject<IEnumerable<Currencies>>(data);

            var tableA = currencies.First();
            var rates = tableA.Rates;

            var availableCurrencies = new List<string>();

            foreach(var rate in rates)
            {
                availableCurrencies.Add($"{rate.Currency}, code: {rate.Code}, mid: {rate.Mid}");
            }

            var result = String.Join("</br>", availableCurrencies.Take(5).ToArray());

            return new ContentResult
            {
                ContentType = "text/html; charset=utf-8",
                Content = result,
            };
        }

        
    }
}
