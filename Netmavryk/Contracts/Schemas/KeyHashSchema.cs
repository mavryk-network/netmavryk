﻿using System.Text.Json;
using Netmavryk.Encoding;

namespace Netmavryk.Contracts
{
    public sealed class KeyHashSchema : Schema, IFlat
    {
        public override PrimType Prim => PrimType.key_hash;

        public KeyHashSchema(MichelinePrim micheline) : base(micheline) { }

        internal override void WriteValue(Utf8JsonWriter writer, IMicheline value)
        {
            writer.WriteStringValue(Flatten(value));
        }

        public string Flatten(IMicheline value)
        {
            if (value is MichelineString micheString)
            {
                return micheString.Value;
            }
            else if (value is MichelineBytes micheBytes)
            {
                if (micheBytes.Value.Length != 21)
                    return Hex.Convert(micheBytes.Value);

                var prefix = micheBytes.Value[0] switch
                {
                    0 => Prefix.mv1,
                    1 => Prefix.mv2,
                    2 => Prefix.mv3,
                    3 => Prefix.mv4,
                    _ => null
                };

                if (prefix == null)
                    return Hex.Convert(micheBytes.Value);

                var bytes = micheBytes.Value.GetBytes(1, micheBytes.Value.Length - 1);
                return Base58.Convert(bytes, prefix);
            }
            else
            {
                throw FormatException(value);
            }
        }

        protected override IMicheline MapValue(object value)
        {
            return value switch
            {
                string str => new MichelineString(str),
                byte[] bytes => new MichelineBytes(bytes),
                JsonElement { ValueKind: JsonValueKind.String } json => new MichelineString(json.GetString()!),
                _ => throw MapFailedException("invalid value"),
            };
        }
        
        public override IMicheline Optimize(IMicheline value)
        {
            if (value is MichelineString micheStr)
            {
                var bytes = Base58.Parse(micheStr.Value, 3);
                var res = new byte[21];

                res[0] = micheStr.Value.Substring(0, 3) switch
                {
                    "mv1" => 0,
                    "mv2" => 1,
                    "mv3" => 2,
                    "mv4" => 3,
                    _ => throw FormatException(value)
                };

                bytes.CopyTo(res, 1);
                return new MichelineBytes(res);
            }

            return value;
        }
    }
}
