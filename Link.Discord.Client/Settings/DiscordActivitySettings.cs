// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;

namespace Link.Discord.Client.Settings
{
    public sealed record DiscordActivitySettings
    {
        public string Name { get; init; } = string.Empty;
        public ActivityType ActivityType { get; init; }
        public string Details { get; init; } = string.Empty;
    }
}
