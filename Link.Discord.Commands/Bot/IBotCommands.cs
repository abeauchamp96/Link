// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

namespace Link.Discord.Commands.Bot
{
    public interface IBotCommands
    {
        string Ping(int latency);
        string Uptime();
    }
}
