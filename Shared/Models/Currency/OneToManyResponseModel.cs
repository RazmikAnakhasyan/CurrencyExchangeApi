﻿using System.Text.Json.Serialization;

namespace Shared.Models.Currency
{
    public class OneToManyResponseModel
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("base")]
        public string Base { get; set; }
        [JsonPropertyName("date")]
        public string Date { get; set; }
        [JsonPropertyName("rates")]
        public Dictionary<string,decimal> Rates { get;set; }
    }
}


