// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord.Commands;
using Link.Discord.Bots.Models;

namespace Link.Discord.Bots
{
    internal static class DiscordCommandRunModeMapper
    {
        internal static RunMode ToRunMode(this DiscordCommandRunMode discordCommandRunMode) => discordCommandRunMode switch
        {
            DiscordCommandRunMode.Async => RunMode.Async,
            DiscordCommandRunMode.Sync => RunMode.Sync,
            DiscordCommandRunMode.Default => RunMode.Default,
            _ => RunMode.Default,
        };
    }
}
