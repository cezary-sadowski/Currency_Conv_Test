using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Currency_Conv_Test
{
    public static class CurrenciesService
    {
        public static async Task<string> GetCurrencies()
        {
            var httpClient = HttpClientFactory.Create();

            var url = "http://api.nbp.pl/api/exchangerates/tables/a/";
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var content = httpResponseMessage.Content;
                var data = await content.ReadAsStringAsync();
                return data;
            }

            return httpResponseMessage.StatusCode.ToString();
        }

        public static string GetAvailableCurrencies()
        {
            var data = CurrenciesService.GetCurrencies().Result;
            var currencies = JsonConvert.DeserializeObject<IEnumerable<Currencies>>(data);

            var tableA = currencies.First();
            var rates = tableA.Rates;

            var availableCurrencies = new List<string>();

            foreach (var rate in rates)
            {
                availableCurrencies.Add($"{rate.Currency}, code: {rate.Code}, mid: {rate.Mid}");
            }

            return String.Join("</br>", availableCurrencies.ToArray());
        }
    }
}
