﻿using Netmavryk.Encoding;
using Netmavryk.Forging.Models;
using Netmavryk.Rpc;

namespace Netmavryk.Forging
{
    public class RpcForge : IForge
    {
        readonly MavrykRpc Rpc;
        
        public RpcForge(MavrykRpc rpc) => Rpc = rpc;

        public Task<byte[]> ForgeOperationAsync(OperationContent content)
            => ForgeAsync(new List<object> { content });

        public Task<byte[]> ForgeOperationAsync(string branch, OperationContent content)
            => ForgeAsync(branch, new List<object> { content });

        public Task<byte[]> ForgeOperationGroupAsync(IEnumerable<ManagerOperationContent> contents)
            => ForgeAsync(contents.Cast<object>().ToList());

        public Task<byte[]> ForgeOperationGroupAsync(string branch, IEnumerable<ManagerOperationContent> contents)
            => ForgeAsync(branch, contents.Cast<object>().ToList());

        async Task<byte[]> ForgeAsync(List<object> contents)
        {
            var branch = await Rpc.Blocks.Head.Hash.GetAsync<string>()
                ?? throw new InvalidOperationException("Branch cannot be null");

            return await ForgeAsync(branch, contents);
        }

        async Task<byte[]> ForgeAsync(string branch, List<object> contents)
        {
            var result = await Rpc
                .Blocks
                .Head
                .Helpers
                .Forge
                .Operations
                .PostAsync<string>(branch, contents)
                ?? throw new InvalidOperationException("Forged bytes cannot be null");

            return Hex.Parse(result);
        }
    }
}
