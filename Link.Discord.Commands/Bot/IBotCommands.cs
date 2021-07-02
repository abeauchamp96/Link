// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;
using Discord.Commands;
using System.Collections.Generic;

namespace Link.Discord.Commands.Bot
{
    public interface IBotCommands
    {
        Embed Help(string? commandName, IEnumerable<CommandInfo> commands);
        Embed Info();
        string Ping(int latency);
        string Uptime();
    }
}
