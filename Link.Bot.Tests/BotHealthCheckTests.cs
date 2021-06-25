// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using Pandora.TestKit.Mocks;
using System.Threading.Tasks;
using Xunit;

namespace Link.Bot
{
    public class BotHealthCheckTests
    {
        private readonly Mock<IBotConnector> botConnectorMock = new();

        private readonly BotSettings botSettings;
        private readonly IHealthCheck healthCheck;

        public BotHealthCheckTests()
        {
            this.botSettings = new BotSettings
            {
                Name = "Link",
                Version = "0.1.0"
            };

            OptionsMock<BotSettings> botSettingsMock = new();
            botSettingsMock.GivenValue(this.botSettings);

            this.healthCheck = new BotHealthCheck(this.botConnectorMock.Object, botSettingsMock.Instance);
        }

        [Fact]
        public async Task CheckHealthAsync_ShouldReturnHealthy_WhenBotIsConnected()
        {
            this.botConnectorMock.SetupGet(b => b.State).Returns(BotConnectionState.Connected);

            var result = await this.healthCheck.CheckHealthAsync(new HealthCheckContext());

            result.Description.Should().Be($"{this.botSettings.Name} ({this.botSettings.Version}) is connected");
            result.Status.Should().Be(HealthStatus.Healthy);
        }

        [Fact]
        public async Task CheckHealthAsync_ShouldReturnUnhealthy_WhenBotIsDisconnected()
        {
            this.botConnectorMock.SetupGet(b => b.State).Returns(BotConnectionState.Disconnected);

            var result = await this.healthCheck.CheckHealthAsync(new HealthCheckContext());

            result.Description.Should().Be($"{this.botSettings.Name} ({this.botSettings.Version}) is disconnected");
            result.Status.Should().Be(HealthStatus.Unhealthy);
        }

        [Fact]
        public async Task CheckHealthAsync_ShouldReturnUnhealthy_WhenHavingUnknownState()
        {
            this.botConnectorMock.SetupGet(b => b.State).Returns((BotConnectionState)999);

            var result = await this.healthCheck.CheckHealthAsync(new HealthCheckContext());

            result.Description.Should().Be("Received an unknown state for the bot");
            result.Status.Should().Be(HealthStatus.Unhealthy);
        }
    }
}
