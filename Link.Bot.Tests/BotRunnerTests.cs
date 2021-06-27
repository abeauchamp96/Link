// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Bot.Helpers;
using Link.Bot.Settings;
using Microsoft.Extensions.Hosting;
using Moq;
using Pandora.TestKit.Mocks;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Link.Bot
{
    public class BotRunnerTests
    {
        private readonly Mock<IBotConnector> botConnectorMock = new();
        private readonly Mock<IDelayHelper> delayHelperMock = new();

        private readonly IHostedService runner;

        public BotRunnerTests()
        {
            OptionsMock<BotSettings> botSettingsMock = new();
            botSettingsMock.GivenValue(new BotSettings
            {
                RetryAttemptInSeconds = 1
            });

            this.runner = new BotRunner(
                this.botConnectorMock.Object,
                this.delayHelperMock.Object,
                botSettingsMock.Instance);
        }

        [Fact]
        public async Task StopAsync_ShouldDisconnectTheBot()
        {
            await this.runner.StopAsync(CancellationToken.None);

            this.botConnectorMock.Verify(b => b.DisconnectAsync());
        }

        [Fact]
        public async Task ExecuteAsync_ShouldRetryToConnectTheBot_WhenStateIsDisconnected()
        {
            this.botConnectorMock.SetupGet(b => b.State).Returns(BotConnectionState.Disconnected);

            await this.RunBackgroundServiceAsync();

            this.botConnectorMock.Verify(b => b.DisconnectAsync());
            this.botConnectorMock.Verify(b => b.ConnectAsync());
        }

        [Fact]
        public async Task ExecuteAsync_ShouldNotRetryToConnectTheBot_WhenStateIsConnected()
        {
            this.botConnectorMock.SetupGet(b => b.State).Returns(BotConnectionState.Connected);

            await this.RunBackgroundServiceAsync();

            this.botConnectorMock.Verify(b => b.DisconnectAsync(), Times.Never);
            this.botConnectorMock.Verify(b => b.ConnectAsync(), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldOnlyDoOneConnection_WhenStateIsDisconnected()
        {
            this.botConnectorMock.SetupSequence(b => b.State)
                .Returns(BotConnectionState.Disconnected)
                .Returns(BotConnectionState.Connected);

            await this.RunBackgroundServiceAsync();

            this.botConnectorMock.Verify(b => b.DisconnectAsync(), Times.Exactly(1));
            this.botConnectorMock.Verify(b => b.ConnectAsync(), Times.Exactly(1));
        }

        [Fact]
        public async Task ExecuteAsync_ShouldDelayTheLoop()
        {
            await this.RunBackgroundServiceAsync();

            this.delayHelperMock.Verify(d => d.DelayAsync(It.Is<TimeSpan>(t => t.TotalSeconds == 1)));
        }

        private Task RunBackgroundServiceAsync()
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(1000);

            return this.runner.StartAsync(source.Token);
        }
    }
}
