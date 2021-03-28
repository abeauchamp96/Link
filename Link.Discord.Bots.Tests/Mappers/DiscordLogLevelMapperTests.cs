// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;
using FluentAssertions;
using Link.Discord.Bots.Models;
using Xunit;

namespace Link.Discord.Bots.Mappers
{
    public class DiscordLogLevelMapperTests
    {
        [Theory]
        [MemberData(nameof(GetDiscordLogLevels))]
        public void ToLogLevel_ShouldConvertTheModel(DiscordLogLevel discordLogLevel, LogSeverity expectedLogSeverity)
        {
            var logLevel = discordLogLevel.ToLogLevel();

            logLevel.Should().Be(expectedLogSeverity);
        }

        [Fact]
        public void ToLogLevel_ShouldReturnVerbose_WhenDoesNotRecognizeTheLogLevel()
        {
            var model = (DiscordLogLevel)999;

            var logLevel = model.ToLogLevel();

            logLevel.Should().Be(LogSeverity.Verbose);
        }

        public static TheoryData<DiscordLogLevel, LogSeverity> GetDiscordLogLevels() => new()
        {
            { DiscordLogLevel.Critical, LogSeverity.Critical },
            { DiscordLogLevel.Debug, LogSeverity.Debug },
            { DiscordLogLevel.Error, LogSeverity.Error },
            { DiscordLogLevel.Info, LogSeverity.Info },
            { DiscordLogLevel.Verbose, LogSeverity.Verbose },
            { DiscordLogLevel.Warning, LogSeverity.Warning }
        };
    }
}
