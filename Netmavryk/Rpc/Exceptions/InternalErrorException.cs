﻿using System.Net;

namespace Netmavryk.Rpc
{
    /// <summary>
    /// Represents the RPC error with HTTP status code 500
    /// </summary>
    public class InternalErrorException : RpcException
    {
        public InternalErrorException(string message) : base(HttpStatusCode.InternalServerError, message) { }
    }
}
