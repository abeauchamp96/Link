// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;
using Link.Discord.Bots.Models;

namespace Link.Discord.Bots.Mappers
{
    internal static class DiscordActivityTypeMapper
    {
        public static ActivityType ToActivityType(this DiscordActivityType activityType) => activityType switch
        {
            DiscordActivityType.Custom => ActivityType.CustomStatus,
            DiscordActivityType.Listening => ActivityType.Listening,
            DiscordActivityType.Playing => ActivityType.Playing,
            DiscordActivityType.Streaming => ActivityType.Streaming,
            DiscordActivityType.Watching => ActivityType.Watching,
            _ => ActivityType.CustomStatus
        };
    }
}
