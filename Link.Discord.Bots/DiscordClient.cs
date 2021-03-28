// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Link.Discord.Bots
{
    internal sealed class DiscordClient : IDiscordClient
    {
        private readonly DiscordSocketClient discordSocketClient;

        public DiscordClient(DiscordSocketClient discordSocketClient)
            => this.discordSocketClient = discordSocketClient;

        public event Func<Task> Connected
        {
            add => this.discordSocketClient.Connected += value;
            remove => this.discordSocketClient.Connected -= value;
        }

        public event Func<Exception, Task> Disconnected
        {
            add => this.discordSocketClient.Disconnected += value;
            remove => this.discordSocketClient.Disconnected -= value;
        }

        public event Func<SocketGuild, Task> JoinedGuild
        {
            add => this.discordSocketClient.JoinedGuild += value;
            remove => this.discordSocketClient.JoinedGuild -= value;
        }

        public event Func<SocketGuild, Task> LeftGuild
        {
            add => this.discordSocketClient.LeftGuild += value;
            remove => this.discordSocketClient.LeftGuild -= value;
        }

        public event Func<LogMessage, Task> Log
        {
            add => this.discordSocketClient.Log += value;
            remove => this.discordSocketClient.Log -= value;
        }

        public event Func<SocketMessage, Task> MessageReceived
        {
            add => this.discordSocketClient.MessageReceived += value;
            remove => this.discordSocketClient.MessageReceived -= value;
        }

        public event Func<Task> Ready
        {
            add => this.discordSocketClient.Ready += value;
            remove => this.discordSocketClient.Ready -= value;
        }

        public async Task ConnectAsync(string token)
        {
            if (this.discordSocketClient.LoginState == LoginState.LoggedOut)
                await this.discordSocketClient.LoginAsync(TokenType.Bot, token).ConfigureAwait(false);

            if (this.discordSocketClient.ConnectionState == ConnectionState.Disconnected)
                await this.discordSocketClient.StartAsync().ConfigureAwait(false);
        }

        public async Task DisconnectAsync()
        {
            if (this.discordSocketClient.LoginState == LoginState.LoggedOut)
                await this.discordSocketClient.LogoutAsync().ConfigureAwait(false);

            if (this.discordSocketClient.ConnectionState == ConnectionState.Disconnected)
                await this.discordSocketClient.StopAsync().ConfigureAwait(false);
        }
    }
}
