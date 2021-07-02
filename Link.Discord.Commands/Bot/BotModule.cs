// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord;
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

        [Command("info")]
        [Alias("bot")]
        [Summary("Gives the information about the bot")]
        public Task InfoAsync()
            => this.ReplyAsync(embed: this.botCommands.Info());

        [Command("ping")]
        [Alias("pong", "latency")]
        [Summary("Ping the server to retrieve the latency")]
        public Task PingAsync()
            => this.ReplyAsync(this.botCommands.Ping(this.Context.Client.Latency));

        [Command("uptime")]
        [Summary("Gives uptime of the bot")]
        public Task UptimeAsync()
        {
            var formattedUptime = Format.Code(this.botCommands.Uptime());
            return this.ReplyAsync(formattedUptime);
        }
    }
}
