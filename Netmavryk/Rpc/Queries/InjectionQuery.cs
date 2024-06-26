﻿using Netmavryk.Rpc.Queries.Post;

namespace Netmavryk.Rpc.Queries
{
    /// <summary>
    /// RPC query to access injections
    /// </summary>
    public class InjectionQuery : RpcObject
    {
        /// <summary>
        /// Gets the query to the block injection
        /// </summary>
        public InjectBlockQuery Block => new(this, "block/");
        
        /// <summary>
        /// Gets the query to the operation injection
        /// </summary>
        public InjectOperationQuery Operation => new(this, "operation/");
        
        /// <summary>
        /// Gets the query to the protocol injection
        /// </summary>
        public InjectProtocolQuery Protocol => new(this, "protocol/");

        internal InjectionQuery(RpcClient client, string query) : base(client, query) { }
    }
}
