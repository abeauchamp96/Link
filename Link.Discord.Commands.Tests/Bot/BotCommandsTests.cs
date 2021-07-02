// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using FluentAssertions;
using Link.Discord.Utility;
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

        private readonly IBotCommands botCommands;

        public BotCommandsTests()
        {
            this.botCommands = new BotCommands(this.uptimeRetrieverMock.Object, this.dateTimeOffsetHelperMock.Object);
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
    }
}
