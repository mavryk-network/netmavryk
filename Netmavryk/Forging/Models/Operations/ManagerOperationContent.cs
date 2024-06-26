﻿using System.Text.Json.Serialization;

namespace Netmavryk.Forging.Models
{
    [JsonConverter(typeof(ManagerOperationContentConverter))]
    public abstract class ManagerOperationContent : OperationContent
    {
        [JsonPropertyName("source")]
        public string Source { get; set; } = null!;

        [JsonPropertyName("fee")]
        [JsonConverter(typeof(Int64StringConverter))]
        public long Fee { get; set; }

        [JsonPropertyName("counter")]
        [JsonConverter(typeof(Int32StringConverter))]
        public int Counter { get; set; }

        [JsonPropertyName("gas_limit")]
        [JsonConverter(typeof(Int32StringConverter))]
        public int GasLimit { get; set; }

        [JsonPropertyName("storage_limit")]
        [JsonConverter(typeof(Int32StringConverter))]
        public int StorageLimit { get; set; }
    }
}
