﻿using System.Text.Json;
using Netmavryk.Encoding;

namespace Netmavryk.Contracts
{
    public sealed class ParameterSchema : Schema
    {
        public override PrimType Prim => PrimType.parameter;

        public override string Signature => Schema.Signature;

        public Schema Schema { get; }

        public ParameterSchema(MichelinePrim micheline) : base(micheline)
        {
            if (micheline.Args?.Count != 1
                || micheline.Args[0] is not MichelinePrim parameter)
                throw new FormatException($"Invalid {Prim} schema format");

            Schema = Create(parameter);
        }

        internal override void WriteProperty(Utf8JsonWriter writer)
        {
            Schema.WriteProperty(writer);
        }

        internal override void WriteProperty(Utf8JsonWriter writer, IMicheline value)
        {
            Schema.WriteProperty(writer, value);
        }
        
        internal override void WriteValue(Utf8JsonWriter writer)
        {
            Schema.WriteValue(writer);
        }

        internal override void WriteJsonSchema(Utf8JsonWriter writer)
        {
            Schema.WriteJsonSchema(writer);
        }

        internal override void WriteValue(Utf8JsonWriter writer, IMicheline value)
        {
            Schema.WriteValue(writer, value);
        }

        protected override List<IMicheline> GetArgs()
        {
            return new List<IMicheline>(1) { Schema.ToMicheline() };
        }

        public override IMicheline Optimize(IMicheline value)
        {
            return Schema.Optimize(value);
        }
    }
}
