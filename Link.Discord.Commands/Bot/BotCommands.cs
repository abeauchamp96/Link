// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;
using Link.Bot.Settings;
using Link.Discord.Utility;
using Microsoft.Extensions.Options;
using Pandora.Utility;
using Pandora.Utility.Helpers;

namespace Link.Discord.Commands.Bot
{
    internal sealed class BotCommands : IBotCommands
    {
        private readonly IUptimeRetriever<DiscordBotUptime> uptimeRetriever;
        private readonly IDateTimeOffsetHelper dateTimeOffsetHelper;
        private readonly BotSettings botSettings;

        public BotCommands(IUptimeRetriever<DiscordBotUptime> uptimeRetriever, IDateTimeOffsetHelper dateTimeOffsetHelper, IOptions<BotSettings> botSettings)
        {
            this.uptimeRetriever = uptimeRetriever;
            this.dateTimeOffsetHelper = dateTimeOffsetHelper;
            this.botSettings = botSettings.Value;
        }

        public Embed Info()
        {
            var builder = new EmbedBuilder()
                .WithColor(Color.Green)
                .WithTitle("Bot information")
                .WithDescription("Hyah!")
                .WithFields(BuildFields());

            return builder.Build();

            EmbedFieldBuilder[] BuildFields() => new[]
            {
                    new EmbedFieldBuilder()
                    .WithName("Name")
                    .WithValue(this.botSettings.Name),

                    new EmbedFieldBuilder()
                    .WithName("Version")
                    .WithValue(this.botSettings.Version),

                    new EmbedFieldBuilder()
                    .WithName("Uptime")
                    .WithValue(this.Uptime()),

                    new EmbedFieldBuilder()
                    .WithName("Project")
                    .WithValue(this.botSettings.ProjectUrl)
            };
        }

        public string Ping(int latency) => $"{EmojiUnicode.Pong} *`({latency}ms)`*";

        public string Uptime()
        {
            var now = this.dateTimeOffsetHelper.GetNowOffset();
            return this.uptimeRetriever.RetrieveFormattedUptime(now);
        }
    }
}
