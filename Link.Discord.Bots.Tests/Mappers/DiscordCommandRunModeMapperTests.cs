// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord.Commands;
using FluentAssertions;
using Link.Discord.Bots.Models;
using Xunit;

namespace Link.Discord.Bots.Mappers
{
    public class DiscordCommandRunModeMapperTests
    {
        [Theory]
        [MemberData(nameof(GetDiscordCommandRunModes))]
        public void ToRunMode_ShouldConvertTheModel(DiscordCommandRunMode discordCommandRunMode, RunMode commandRunMode)
        {
            var mode = discordCommandRunMode.ToRunMode();

            mode.Should().Be(commandRunMode);
        }

        [Fact]
        public void ToRunMode_ShouldReturnDefault_WhenDoesNotRecognizeTheRunMode()
        {
            var model = (DiscordCommandRunMode)999;

            var runMode = model.ToRunMode();

            runMode.Should().Be(RunMode.Default);
        }

        public static TheoryData<DiscordCommandRunMode, RunMode> GetDiscordCommandRunModes() => new()
        {
            { DiscordCommandRunMode.Async, RunMode.Async },
            { DiscordCommandRunMode.Sync, RunMode.Sync },
            { DiscordCommandRunMode.Default, RunMode.Default }
        };
    }
}
