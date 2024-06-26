﻿using Netmavryk.Rpc.Queries.Post;

namespace Netmavryk.Rpc.Queries
{
    /// <summary>
    /// RPC query to get data parsing helpers associated with a block
    /// </summary>
    public class ParseQuery : RpcQuery
    {
        /// <summary>
        /// Gets the query to the block parsing
        /// </summary>
        public ParseBlockQuery Block => new(this, "block/");

        /// <summary>
        /// Gets the query to the operations parsing
        /// </summary>
        public ParseOperationsQuery Operations => new(this, "operations/");

        internal ParseQuery(RpcQuery baseQuery, string append) : base(baseQuery, append) { }
    }
}
