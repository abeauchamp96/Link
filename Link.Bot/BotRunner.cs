// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Link.Bot
{
    internal sealed class BotRunner : IHostedService
    {
        private readonly IBotConnector botConnector;

        public BotRunner(IBotConnector botConnector) => this.botConnector = botConnector;

        public Task StartAsync(CancellationToken cancellationToken)
            => this.botConnector.ConnectAsync();

        public Task StopAsync(CancellationToken cancellationToken)
            => this.botConnector.DisconnectAsync();
    }
}
