// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Discord.Bots;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Link.Bot
{
    public class BotRunner : IHostedService
    {
        private readonly IDiscordClient discordClient;
        private readonly BotSettings botSettings;

        public BotRunner(IDiscordClient discordClient, IOptions<BotSettings> botSettings)
        {
            this.discordClient = discordClient;
            this.botSettings = botSettings.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
            => this.discordClient.ConnectAsync(this.botSettings.Token);

        public Task StopAsync(CancellationToken cancellationToken)
            => this.discordClient.DisconnectAsync();
    }
}
