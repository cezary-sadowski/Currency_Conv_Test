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
        private static readonly string currenciesList = "http://api.nbp.pl/api/exchangerates/tables/a/";
        public static async Task<string> GetCurrenciesFromAPI()
        {
            var httpClient = HttpClientFactory.Create();
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(currenciesList);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var content = httpResponseMessage.Content;
                var data = await content.ReadAsStringAsync();
                return data;
            }

            return httpResponseMessage.StatusCode.ToString();
        }

        public static Rate[] GetAvailableCurrenciesFromJson()
        {
            var data = CurrenciesService.GetCurrenciesFromAPI().Result;
            var currencies = JsonConvert.DeserializeObject<IEnumerable<Currencies>>(data);

            var tableA = currencies.First();
            return tableA.Rates;
        }

        public static string GetListOfCurrenciesWithCode()
        {
            var rates = GetAvailableCurrenciesFromJson();
            var availableCurrencies = new List<string>();

            foreach (var rate in rates)
            {
                availableCurrencies.Add($"{rate.Currency}, Currency Code: <b>{rate.Code}</b>");
            }

            return String.Join("</br>", availableCurrencies.ToArray());
        }

        public static string GetCurrentRateForCurrency(string code)
        {
            var rates = GetAvailableCurrenciesFromJson().ToDictionary(k => k.Code, v => v.Mid);

            return rates.SingleOrDefault(k => k.Key.Equals(code)).Value.ToString();
        }
    }
}
