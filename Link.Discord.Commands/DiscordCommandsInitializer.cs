// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Link.Discord.Commands.Bot;
using Link.Discord.Commands.Misc;
using Link.Discord.Utility.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Link.Discord.Commands
{
    internal sealed class DiscordCommandsInitializer : IDiscordCommandsInitializer
    {
        private readonly CommandService commandService;
        private readonly DiscordSocketClient discordSocketClient;
        private readonly DiscordSettings discordSettings;
        private readonly IServiceProvider serviceProvider;

        public DiscordCommandsInitializer(
            CommandService commandService,
            DiscordSocketClient discordSocketClient,
            IOptions<DiscordSettings> discordSettings,
            IServiceProvider serviceProvider)
        {
            this.commandService = commandService;
            this.discordSocketClient = discordSocketClient;
            this.discordSettings = discordSettings.Value;
            this.serviceProvider = serviceProvider;
        }

        public async Task InitializeAsync()
        {
            this.discordSocketClient.MessageReceived += this.MessageReceivedAsync;

            await this.commandService.AddModuleAsync<BotModule>(this.serviceProvider);
            await this.commandService.AddModuleAsync<MiscModule>(this.serviceProvider);
        }

        public void Uninitialized()
        {
            this.discordSocketClient.MessageReceived -= this.MessageReceivedAsync;
        }

        private Task MessageReceivedAsync(SocketMessage socketMessage)
        {
            if (socketMessage is not SocketUserMessage message || message.Source != MessageSource.User)
                return Task.CompletedTask;

            var commandContext = new SocketCommandContext(this.discordSocketClient, message);

            var argPos = 0;

            if (!message.HasCharPrefix(this.discordSettings.Prefix, ref argPos))
                return Task.CompletedTask;

            return this.commandService.ExecuteAsync(commandContext, argPos, this.serviceProvider);
        }
    }
}
