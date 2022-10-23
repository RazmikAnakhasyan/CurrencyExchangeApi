using System.Text.Json.Serialization;

namespace Shared.Models.Currency
{
    public class CurrencyConvertResponse
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }
        [JsonPropertyName("info")]
        public Info Info { get; set; }
        [JsonPropertyName("query")]
        public Query Query { get; set; }
        [JsonPropertyName("result")]
        public double Result { get; set; }
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }

    public class Info
    {
        [JsonPropertyName("rate")]
        public double Rate { get; set; }
    }

    public class Query
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("from")]
        public string From { get; set; }
        [JsonPropertyName("to")]
        public string To { get; set; }
    }

}
