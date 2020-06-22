using System;
using Newtonsoft.Json;

namespace Currency_Conv_Test
{
    public partial class CurrencyConverter
    {
        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("no")]
        public string No { get; set; }

        [JsonProperty("effectiveDate")]
        public DateTimeOffset EffectiveDate { get; set; }

        [JsonProperty("rates")]
        public Rate[] Rates { get; set; }
    }

    public partial class Rate
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("mid")]
        public double Mid { get; set; }
    }
}
