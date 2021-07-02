// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Discord.Utility;
using Pandora.Utility;
using Pandora.Utility.Helpers;

namespace Link.Discord.Commands.Bot
{
    internal sealed class BotCommands : IBotCommands
    {
        private readonly IUptimeRetriever<DiscordBotUptime> uptimeRetriever;
        private readonly IDateTimeOffsetHelper dateTimeOffsetHelper;

        public BotCommands(IUptimeRetriever<DiscordBotUptime> uptimeRetriever, IDateTimeOffsetHelper dateTimeOffsetHelper)
        {
            this.uptimeRetriever = uptimeRetriever;
            this.dateTimeOffsetHelper = dateTimeOffsetHelper;
        }

        public string Ping(int latency) => $"{EmojiUnicode.Pong} *`({latency}ms)`*";

        public string Uptime()
        {
            var now = this.dateTimeOffsetHelper.GetNowOffset();
            return this.uptimeRetriever.RetrieveFormattedUptime(now);
        }
    }
}
