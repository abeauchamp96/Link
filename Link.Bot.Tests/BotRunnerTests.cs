// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Discord.Bots;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Link.Bot
{
    public class BotRunnerTests
    {
        private readonly Mock<IDiscordClient> discordClientMock = new();

        private readonly BotSettings botSettings;
        private readonly IHostedService hostedService;

        public BotRunnerTests()
        {
            this.botSettings = new()
            {
                Token = "token"
            };

            Mock<IOptions<BotSettings>> botSettingsMock = new();
            botSettingsMock.SetupGet(b => b.Value).Returns(this.botSettings);

            this.hostedService = new BotRunner(this.discordClientMock.Object, botSettingsMock.Object);
        }

        [Fact]
        public async Task StartAsync_ShouldConnectTheBotUsingTheClient()
        {
            await this.hostedService.StartAsync(CancellationToken.None);

            this.discordClientMock.Verify(d => d.ConnectAsync(this.botSettings.Token));
        }

        [Fact]
        public async Task StopAsync_ShouldDisconnectTheBotUsingTheClient()
        {
            await this.hostedService.StopAsync(CancellationToken.None);

            this.discordClientMock.Verify(d => d.DisconnectAsync());
        }
    }
}
