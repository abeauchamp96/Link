// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;
using FluentAssertions;
using Xunit;
namespace Link.Discord.Bots.Mappers
{
    public class DiscordActivityMapperTests
    {
        [Fact]
        public void ToActivity_ShouldConvertTheModel()
        {
            DiscordActivity model = new()
            {
                ActivityType = Models.DiscordActivityType.Playing,
                Name = "Breath of the Wild",
                Details = "Some details"
            };

            var activity = model.ToGameActivity();

            activity.Name.Should().Be(model.Name);
            activity.Type.Should().Be(ActivityType.Playing);
            activity.Flags.Should().Be(ActivityProperties.Instance);
            activity.Details.Should().Be(model.Details);
        }
    }
}
