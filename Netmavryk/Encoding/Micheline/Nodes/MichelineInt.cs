﻿using System.Numerics;
using System.Text.Json.Serialization;
using Netmavryk.Forging.Models;

namespace Netmavryk.Encoding
{
    public class MichelineInt : IMicheline
    {
        [JsonIgnore]
        public MichelineType Type => MichelineType.Int;

        [JsonPropertyName("int")]
        [JsonConverter(typeof(BigIntegerStringConverter))]
        public BigInteger Value { get; set; }

        public MichelineInt(BigInteger value) => Value = value;

        public void Write(BinaryWriter writer, int depth = 0)
        {
            var bytes = Value.ToByteArray();
            var len = bytes.Length;

            if (len >= 0x1F)
            {
                writer.Write((byte)((int)Type | 0x1F));
                writer.Write7BitInt(len);
            }
            else
            {
                writer.Write((byte)((int)Type | len));
            }

            writer.Write(bytes);
        }
    }
}
