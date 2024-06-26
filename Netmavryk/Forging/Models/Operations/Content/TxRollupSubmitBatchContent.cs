﻿using System.Text.Json.Serialization;

namespace Netmavryk.Forging.Models
{
    public class TxRollupSubmitBatchContent : ManagerOperationContent
    {
        [JsonPropertyName("kind")]
        public override string Kind => "tx_rollup_submit_batch";

        [JsonPropertyName("rollup")]
        public string Rollup { get; set; } = null!;

        [JsonPropertyName("content")]
        [JsonConverter(typeof(HexConverter))]
        public byte[] Content { get; set; } = null!;

        [JsonPropertyName("burn_limit")]
        [JsonConverter(typeof(Int64StringConverter))]
        public long? BurnLimit { get; set; }
    }
}
