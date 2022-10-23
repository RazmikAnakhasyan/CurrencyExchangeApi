using System.Text.Json.Serialization;

namespace Shared.Models.Currency
{
    public class OneToManyConvertModel
    {
        [JsonPropertyName("symbols")]
        public string Symbols { get; set; }
        [JsonPropertyName("base")]
        public string Base { get; set; }
    }
}
