// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord.Commands;
using System.Threading.Tasks;

namespace Link.Discord.Commands.Bot
{
    public sealed class BotModule : ModuleBase<SocketCommandContext>
    {
        private readonly IBotCommands botCommands;

        public BotModule(IBotCommands botCommands)
        {
            this.botCommands = botCommands;
        }

        [Command("ping")]
        [Alias("pong", "latency")]
        [Summary("Ping the server to retrieve the latency")]
        public Task PingAsync() 
            => this.ReplyAsync(this.botCommands.Ping(this.Context.Client.Latency));
    }
}
