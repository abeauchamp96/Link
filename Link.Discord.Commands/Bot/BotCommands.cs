// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Discord.Utility;

namespace Link.Discord.Commands.Bot
{
    internal sealed class BotCommands : IBotCommands
    {
        public string Ping(int latency) => $"{EmojiUnicode.Pong} *`({latency}ms)`*";
    }
}
