// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;
using Discord.WebSocket;
using Link.Bot;
using Link.Bot.Settings;
using Link.Discord.Commands;
using Link.Discord.Utility.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Link.Discord.Client
{
    internal sealed class DiscordBotConnector : IBotConnector, IDisposable
    {
        private readonly DiscordSocketClient discordSocketClient;
        private readonly IDiscordCommandsInitializer discordCommandsInitializer;
        private readonly ILogger<DiscordBotConnector> logger;
        private readonly DiscordSettings discordSettings;
        private readonly BotSettings botSettings;

        public DiscordBotConnector(
            DiscordSocketClient discordSocketClient,
            IDiscordCommandsInitializer discordCommandsInitializer,
            ILogger<DiscordBotConnector> logger,
            IOptions<DiscordSettings> discordSettings,
            IOptions<BotSettings> botSettings)
        {
            this.discordSocketClient = discordSocketClient;
            this.discordCommandsInitializer = discordCommandsInitializer;
            this.logger = logger;
            this.discordSettings = discordSettings.Value;
            this.botSettings = botSettings.Value;

            this.discordSocketClient.Ready += this.Ready;
            this.discordSocketClient.Connected += this.Connected;
            this.discordSocketClient.Disconnected += this.Disconnected;
        }

        public BotConnectionState State => this.discordSocketClient.ConnectionState switch
        {
            ConnectionState.Disconnected => BotConnectionState.Disconnected,
            ConnectionState.Connecting => BotConnectionState.Disconnected,
            ConnectionState.Connected => BotConnectionState.Connected,
            ConnectionState.Disconnecting => BotConnectionState.Connected,
            _ => throw new InvalidOperationException()
        };

        public async Task ConnectAsync()
        {
            await this.discordCommandsInitializer.InitializeAsync().ConfigureAwait(false);

            if (this.discordSocketClient.LoginState == LoginState.LoggedOut)
                await this.discordSocketClient.LoginAsync(TokenType.Bot, this.discordSettings.Token).ConfigureAwait(false);

            if (this.discordSocketClient.ConnectionState == ConnectionState.Disconnected)
                await this.discordSocketClient.StartAsync().ConfigureAwait(false);
        }

        public async Task DisconnectAsync()
        {
            if (this.discordSocketClient.LoginState == LoginState.LoggedIn)
                await this.discordSocketClient.LogoutAsync().ConfigureAwait(false);

            if (this.discordSocketClient.ConnectionState == ConnectionState.Connected)
                await this.discordSocketClient.StopAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            this.discordSocketClient.Ready -= this.Ready;
            this.discordSocketClient.Connected -= this.Connected;
            this.discordSocketClient.Disconnected -= this.Disconnected;
            this.discordCommandsInitializer.Uninitialized();
        }

        private async Task Ready()
        {
            this.logger.LogInformation($"{this.GetName()} is ready");

            if (this.discordSettings.Activity is null)
                return;

            await this.discordSocketClient.SetActivityAsync(GetActivity());

            IActivity GetActivity()
            {
                var activity = this.discordSettings.Activity;
                return new Game(activity.Name, activity.Type);
            }
        }

        private Task Connected()
        {
            this.logger.LogInformation($"{this.GetName()} is connected");
            return Task.CompletedTask;
        }

        private Task Disconnected(Exception exception)
        {
            this.logger.LogInformation($"{this.GetName()} is disconnected");
            return Task.CompletedTask;
        }

        private string GetName()
            => $"{this.botSettings.Name} ({this.botSettings.Version})";
    }
}
