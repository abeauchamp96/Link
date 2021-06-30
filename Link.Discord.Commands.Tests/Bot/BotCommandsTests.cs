// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using FluentAssertions;
using Link.Discord.Utility;
using Xunit;

namespace Link.Discord.Commands.Bot
{
    public class BotCommandsTests
    {
        private readonly IBotCommands botCommands;

        public BotCommandsTests()
        {
            this.botCommands = new BotCommands();
        }

        [Fact]
        public void Ping_ShouldReturnTheLatency()
        {
            const int latency = 30;

            var message = this.botCommands.Ping(latency);

            message.Should().Be($"{EmojiUnicode.Pong} *`({latency}ms)`*");
        }
    }
}
