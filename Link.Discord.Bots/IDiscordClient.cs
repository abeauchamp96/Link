// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Link.Discord.Bots
{
    public interface IDiscordClient
    {
        event Func<Task> Connected;
        event Func<Exception, Task> Disconnected;
        event Func<SocketGuild, Task> JoinedGuild;
        event Func<SocketGuild, Task> LeftGuild;
        event Func<LogMessage, Task> Log;
        event Func<SocketMessage, Task> MessageReceived;
        event Func<Task> Ready;

        Task ConnectAsync(string token);
        Task DisconnectAsync();
    }
}
