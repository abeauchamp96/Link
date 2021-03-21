// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;

namespace Link.Discord.Bots.Models
{
    internal static class DiscordLogLevelMapper
    {
        internal static LogSeverity ToLogLevel(this DiscordLogLevel discordLogLevel) => discordLogLevel switch
        {
            DiscordLogLevel.Critical => LogSeverity.Critical,
            DiscordLogLevel.Debug => LogSeverity.Debug,
            DiscordLogLevel.Error => LogSeverity.Error,
            DiscordLogLevel.Info => LogSeverity.Info,
            DiscordLogLevel.Verbose => LogSeverity.Verbose,
            DiscordLogLevel.Warning => LogSeverity.Warning,
            _ => LogSeverity.Verbose,
        };
    }
}
