// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;
using FluentAssertions;
using Link.Bot.Settings;
using Link.Discord.Utility;
using Microsoft.Extensions.Options;
using Moq;
using Pandora.Utility;
using Pandora.Utility.Helpers;
using System;
using Xunit;

namespace Link.Discord.Commands.Bot
{
    public class BotCommandsTests
    {
        private readonly Mock<IUptimeRetriever<DiscordBotUptime>> uptimeRetrieverMock = new();
        private readonly Mock<IDateTimeOffsetHelper> dateTimeOffsetHelperMock = new();
        private readonly BotSettings botSettings;

        private readonly IBotCommands botCommands;

        public BotCommandsTests()
        {
            this.botSettings = new BotSettings
            {
                Name = "Link",
                Version = "1.0.0",
                ProjectUrl = new Uri("http://localhost")
            };

            Mock<IOptions<BotSettings>> botSettingsMock = new();
            botSettingsMock.SetupGet(b => b.Value).Returns(this.botSettings);

            this.botCommands = new BotCommands(
                this.uptimeRetrieverMock.Object,
                this.dateTimeOffsetHelperMock.Object,
                botSettingsMock.Object);
        }

        [Fact]
        public void Ping_ShouldReturnTheLatency()
        {
            const int latency = 30;

            var message = this.botCommands.Ping(latency);

            message.Should().Be($"{EmojiUnicode.Pong} *`({latency}ms)`*");
        }

        [Fact]
        public void Uptime_ShouldReturnTheUptime()
        {
            var now = new DateTimeOffset(new DateTime(2021, 07, 01));
            const string uptimeMessage = "uptime";

            this.dateTimeOffsetHelperMock.Setup(d => d.GetNowOffset()).Returns(now);
            this.uptimeRetrieverMock.Setup(u => u.RetrieveFormattedUptime(now)).Returns(uptimeMessage);

            var uptime = this.botCommands.Uptime();

            uptime.Should().Be(uptimeMessage);
        }

        [Fact]
        public void Info_ShouldBuildAnEmbedMessageWhichContainsBotInformation()
        {
            var now = new DateTimeOffset(new DateTime(2021, 07, 01));
            const string uptimeMessage = "uptime";

            this.dateTimeOffsetHelperMock.Setup(d => d.GetNowOffset()).Returns(now);
            this.uptimeRetrieverMock.Setup(u => u.RetrieveFormattedUptime(now)).Returns(uptimeMessage);

            var embed = this.botCommands.Info();

            embed.Color.Should().Be(Color.Green);
            embed.Title.Should().Be("Bot information");
            embed.Description.Should().Be("Hyah!");

            embed.Fields.Should().Contain(f => f.Name == "Name" && f.Value == this.botSettings.Name);
            embed.Fields.Should().Contain(f => f.Name == "Version" && f.Value == this.botSettings.Version);
            embed.Fields.Should().Contain(f => f.Name == "Uptime" && f.Value == uptimeMessage);
            embed.Fields.Should().Contain(f => f.Name == "Project" && f.Value == this.botSettings.ProjectUrl!.ToString());
        }
    }
}
