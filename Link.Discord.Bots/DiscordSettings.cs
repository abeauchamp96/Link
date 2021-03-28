// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Discord.Bots.Models;

namespace Link.Discord.Bots
{
    public sealed record DiscordSettings
    {
        public int MessageCacheSize { get; init; }
        public DiscordLogLevel LogLevel { get; init; }
        public bool CaseSensitiveCommands { get; init; }
        public char ArgumentSeparator { get; init; }
        public DiscordCommandRunMode CommandRunMode { get; init; }
        public DiscordActivity? Activity { get; init; }
    }
}
