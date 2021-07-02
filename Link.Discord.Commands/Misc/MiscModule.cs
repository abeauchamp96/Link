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
        public Task EchoAsync([Remainder] string? message = null)
        {
            var echoMessage = this.miscCommands.Echo(message);
            return this.ReplyAsync(echoMessage);
        }

        [Command("say")]
        [Summary("The bot say something and delete your message")]
        public async Task SayAsync([Remainder] string? message = null)
        {
            var messageToSay = this.miscCommands.Say(message);

            await this.Context.Message.DeleteAsync().ConfigureAwait(false);
            await this.ReplyAsync(messageToSay);
        }

        [Command("flip")]
        [Alias("coin")]
        [Summary("Flip a coin")]
        public Task FlipAsync()
        {
            var flippedCoin = this.miscCommands.Flip();
            return this.ReplyAsync(flippedCoin);
        }

        [Command("lenny")]
        [Summary("( ͡° ͜ʖ ͡°)")]
        public async Task LennyAsync()
        {
            await this.Context.Message.DeleteAsync().ConfigureAwait(false);
            await this.ReplyAsync(this.miscCommands.Lenny());
        }

        [Command("running")]
        [Summary("ᕕ( ᐛ )ᕗ")]
        public async Task RunningAsync()
        {
            await this.Context.Message.DeleteAsync().ConfigureAwait(false);
            await this.ReplyAsync(this.miscCommands.Running());
        }
    }
}
