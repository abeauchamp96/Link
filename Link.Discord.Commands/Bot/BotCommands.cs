// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;
using Discord.Commands;
using Link.Bot.Settings;
using Link.Discord.Utility;
using Link.Discord.Utility.Settings;
using Microsoft.Extensions.Options;
using Pandora.Utility;
using Pandora.Utility.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Link.Discord.Commands.Bot
{
    internal sealed class BotCommands : IBotCommands
    {
        private readonly IUptimeRetriever<DiscordBotUptime> uptimeRetriever;
        private readonly IDateTimeOffsetHelper dateTimeOffsetHelper;
        private readonly BotSettings botSettings;
        private readonly DiscordSettings discordSettings;

        public BotCommands(
            IUptimeRetriever<DiscordBotUptime> uptimeRetriever,
            IDateTimeOffsetHelper dateTimeOffsetHelper,
            IOptions<BotSettings> botSettings,
            IOptions<DiscordSettings> discordSettings)
        {
            this.uptimeRetriever = uptimeRetriever;
            this.dateTimeOffsetHelper = dateTimeOffsetHelper;
            this.botSettings = botSettings.Value;
            this.discordSettings = discordSettings.Value;
        }

        public Embed Help(string? commandName, IEnumerable<CommandInfo> commands)
        {
            var embedBuilder = new EmbedBuilder()
                .WithColor(Color.Green);

            if (string.IsNullOrEmpty(commandName))
            {
                return EmbedAllCommands(embedBuilder, commands);
            }
            else
            {
                var command = commands.FirstOrDefault(c => c.Name == commandName || c.Aliases.Contains(commandName));
                return EmbedCommand(embedBuilder, command);
            }

            static Embed EmbedAllCommands(EmbedBuilder embedBuilder, IEnumerable<CommandInfo> commands)
            {
                embedBuilder.WithTitle("Commands").WithDescription("Here the command list");

                foreach (var command in commands)
                    embedBuilder.AddField(command.Name, command.Summary);

                return embedBuilder.Build();
            }

            Embed EmbedCommand(EmbedBuilder embedBuilder, CommandInfo? command)
            {
                if (command == null)
                    return embedBuilder.WithTitle("This command does not exist").Build();

                embedBuilder.WithTitle($"{command.Name} command").WithDescription(command.Summary);

                if (command.Aliases.Count != 0)
                    embedBuilder.AddField("Aliases", string.Join(", ", command.Aliases));

                embedBuilder.AddField("Usage", Format.Code($"{this.discordSettings.Prefix}{command.Name} {string.Join(' ', command.Parameters)}"));

                return embedBuilder.Build();
            }
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
