using System.Text.Json.Serialization;

namespace Netmavryk.Forging.Models
{
    public class SrRecoverBondContent : ManagerOperationContent
    {
        [JsonPropertyName("kind")]
        public override string Kind => "smart_rollup_recover_bond";

        [JsonPropertyName("rollup")]
        public string Rollup { get; set; } = null!;

        [JsonPropertyName("staker")]
        public string Staker { get; set; } = null!;
    }
}