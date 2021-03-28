// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Discord.Bots.Models;

namespace Link.Discord.Bots
{
    public sealed record DiscordActivity
    {
        public string Name { get; init; } = string.Empty;
        public DiscordActivityType ActivityType { get; init; }
        public string Details { get; init; } = string.Empty;
    }
}
