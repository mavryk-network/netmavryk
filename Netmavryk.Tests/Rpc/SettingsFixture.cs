﻿using Dynamic.Json;
using Netmavryk.Rpc;
using System;

namespace Netmavryk.Tests.Rpc
{
    public class SettingsFixture : IDisposable
    {
        static readonly object Crit = new();

        public MavrykRpc Rpc { get; }
        public string TestContract { get; }
        public string TestEntrypoint { get; }
        public string TestDelegate { get; }
        public string TestInactive { get; }
        public string TestSmartRollup { get; }
        public string KeyHash { get; }
        public int BigMapId { get; }

        public SettingsFixture()
        {
            lock (Crit)
            {
                var settings = DJson.Read("../../../Rpc/settings.json");

                Rpc = new MavrykRpc(settings.node, 60);
                TestContract = settings.TestContract;
                TestEntrypoint = settings.TestEntrypoint;
                TestDelegate = settings.TestDelegate;
                TestInactive = settings.TestInactive;
                TestSmartRollup = settings.TestSmartRollup;
                KeyHash = settings.KeyHash;
                BigMapId = settings.BigMapId;
            }
        }

        public void Dispose()
        {
            Rpc.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
