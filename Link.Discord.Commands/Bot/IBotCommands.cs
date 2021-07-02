// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;

namespace Link.Discord.Commands.Bot
{
    public interface IBotCommands
    {
        Embed Info();
        string Ping(int latency);
        string Uptime();
    }
}
