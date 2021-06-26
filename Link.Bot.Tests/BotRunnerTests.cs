// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Extensions.Hosting;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Link.Bot
{
    public class BotRunnerTests
    {
        private readonly Mock<IBotConnector> botConnectorMock = new();

        private readonly IHostedService runner;

        public BotRunnerTests()
        {
            this.runner = new BotRunner(this.botConnectorMock.Object);
        }

        [Fact]
        public async Task StartAsync_ShouldConnectTheBot()
        {
            await this.runner.StartAsync(CancellationToken.None);

            this.botConnectorMock.Verify(b => b.ConnectAsync());
        }

        [Fact]
        public async Task StopAsync_ShouldDisconnectTheBot()
        {
            await this.runner.StopAsync(CancellationToken.None);

            this.botConnectorMock.Verify(b => b.DisconnectAsync());
        }
    }
}
