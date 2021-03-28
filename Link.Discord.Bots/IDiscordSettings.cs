// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Discord.Bots.Models;

namespace Link.Discord.Bots
{
    public interface IDiscordSettings
    {
        int MessageCacheSize { get; init; }
        DiscordLogLevel LogLevel { get; init; }
        bool CaseSensitiveCommands { get; init; }
        char ArgumentSeparator { get; init; }
        DiscordCommandRunMode CommandRunMode { get; init; }
        DiscordActivity? Activity { get; init; }
    }
}
