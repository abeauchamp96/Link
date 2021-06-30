// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;

namespace Link.Discord.Utility.Settings
{
    public sealed record DiscordActivitySettings
    {
        public string Name { get; init; } = string.Empty;
        public ActivityType Type { get; init; }
    }
}
