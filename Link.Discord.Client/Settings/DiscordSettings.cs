// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;
using Discord.Commands;

namespace Link.Discord.Client.Settings
{
    public sealed record DiscordSettings
    {
        public int MessageCacheSize { get; init; }
        public string Token { get; init; } = string.Empty;
        public LogSeverity LogLevel { get; init; }
        public bool CaseSensitiveCommands { get; init; }
        public char ArgumentSeparator { get; init; }
        public RunMode RunMode { get; init; }
        public DiscordActivitySettings? Activity { get; init; }
    }
}
