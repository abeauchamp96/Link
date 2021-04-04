// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord.WebSocket;
using Link.Discord.Bots.Mappers;
using System.Threading.Tasks;

namespace Link.Discord.Bots
{
    internal sealed class ActivityUpdater : IActivityUpdater
    {
        private readonly DiscordSocketClient discordSocketClient;

        public ActivityUpdater(DiscordSocketClient discordSocketClient)
            => this.discordSocketClient = discordSocketClient;

        public Task UpdateActivityAsync(DiscordActivity discordActivity)
            => this.discordSocketClient.SetActivityAsync(discordActivity.ToGameActivity());
    }
}
