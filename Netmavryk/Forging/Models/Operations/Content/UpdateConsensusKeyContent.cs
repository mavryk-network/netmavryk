﻿using System.Text.Json.Serialization;

namespace Netmavryk.Forging.Models
{
    public class UpdateConsensusKeyContent : ManagerOperationContent
    {
        [JsonPropertyName("kind")]
        public override string Kind => "update_consensus_key";

        [JsonPropertyName("pk")]
        public string PublicKey { get; set; } = null!;
    }
}
