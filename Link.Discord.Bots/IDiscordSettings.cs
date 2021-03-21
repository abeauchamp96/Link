// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Discord.Bots.Models;

namespace Link.Discord.Bots
{
    public interface IDiscordSettings
    {
        int MessageCacheSize { get; }
        DiscordLogLevel LogLevel { get; }
        bool CaseSensitiveCommands { get; }
        char ArgumentSeparator { get; }
        DiscordCommandRunMode CommandRunMode { get; }
    }
}
