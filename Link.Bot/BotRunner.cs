// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Bot.Helpers;
using Link.Bot.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Link.Bot
{
    internal sealed class BotRunner : BackgroundService
    {
        private readonly IBotConnector botConnector;
        private readonly IDelayHelper delayHelper;
        private readonly TimeSpan retryAttemptInSeconds;

        public BotRunner(IBotConnector botConnector, IDelayHelper delayHelper, IOptions<BotSettings> botSettings)
        {
            this.botConnector = botConnector;
            this.delayHelper = delayHelper;
            this.retryAttemptInSeconds = TimeSpan.FromSeconds(botSettings.Value.RetryAttemptInSeconds);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await this.botConnector.DisconnectAsync();
            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (this.botConnector.State == BotConnectionState.Disconnected)
                {
                    await this.botConnector.DisconnectAsync();
                    await this.botConnector.ConnectAsync();
                }

                await this.delayHelper.DelayAsync(this.retryAttemptInSeconds);
            }
        }
    }
}
