// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;
using FluentAssertions;
using Link.Discord.Bots.Models;
using Xunit;

namespace Link.Discord.Bots.Mappers
{
    public class DiscordActivityTypeMapperTests
    {
        [Theory]
        [MemberData(nameof(GetDiscordActivityTypes))]
        public void ToActivityType_ShouldConvertTheModel(DiscordActivityType discordActivityType, ActivityType expectedActivityType)
        {
            var activityType = discordActivityType.ToActivityType();

            activityType.Should().Be(expectedActivityType);
        }

        [Fact]
        public void ToActivityType_ShouldReturnCustom_WhenDoesNotRecognizeTheActivityType()
        {
            var model = (DiscordActivityType)999;

            var activityType = model.ToActivityType();

            activityType.Should().Be(ActivityType.CustomStatus);
        }

        public static TheoryData<DiscordActivityType, ActivityType> GetDiscordActivityTypes() => new()
        {
            { DiscordActivityType.Custom, ActivityType.CustomStatus },
            { DiscordActivityType.Listening, ActivityType.Listening },
            { DiscordActivityType.Playing, ActivityType.Playing },
            { DiscordActivityType.Streaming, ActivityType.Streaming },
            { DiscordActivityType.Watching, ActivityType.Watching }
        };
    }
}
