// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;

namespace Link.Discord.Bots.Mappers
{
    internal static class DiscordActivityMapper
    {
        public static IActivity ToActivity(this DiscordActivity discordActivity)
            => new Game(discordActivity.Name, discordActivity.ActivityType.ToActivityType(), ActivityProperties.Instance, discordActivity.Details);
    }
}
