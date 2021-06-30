// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord.Commands;
using System.Threading.Tasks;

namespace Link.Discord.Commands.Misc
{
    public sealed class MiscModule : ModuleBase<SocketCommandContext>
    {
        private readonly IMiscCommands miscCommands;

        public MiscModule(IMiscCommands miscCommands) => this.miscCommands = miscCommands;

        [Command("echo")]
        [Summary("Repeat the message")]
        public Task EchoAsync([Remainder]string message)
        {
            var echoMessage = this.miscCommands.Echo(message);
            return this.ReplyAsync(echoMessage);
        }

        [Command("flip")]
        [Alias("coin")]
        [Summary("Flip a coin")]
        public Task FlipAsync()
        {
            var flippedCoin = this.miscCommands.Flip();
            return this.ReplyAsync(flippedCoin);
        }
    }
}
