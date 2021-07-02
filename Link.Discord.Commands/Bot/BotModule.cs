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
        private readonly CommandService commandService;

        public BotModule(IBotCommands botCommands, CommandService commandService)
        {
            this.botCommands = botCommands;
            this.commandService = commandService;
        }

        [Command("help")]
        [Summary("Gives information about a command or gives the command list")]
        public Task HelpAsync(string? commandName = null)
            => this.ReplyAsync(embed: this.botCommands.Help(commandName, this.commandService.Commands));

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
