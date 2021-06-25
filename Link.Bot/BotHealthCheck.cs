// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Link.Bot
{
    public class BotHealthCheck : IHealthCheck
    {
        private readonly IBotConnector botConnector;
        private readonly BotSettings botSettings;

        public BotHealthCheck(IBotConnector botConnector, IOptions<BotSettings> botSettings)
        {
            this.botConnector = botConnector;
            this.botSettings = botSettings.Value;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return this.botConnector.State switch
            {
                BotConnectionState.Connected => Task.FromResult(HealthCheckResult.Healthy($"{GetBotName()} is connected")),
                BotConnectionState.Disconnected => Task.FromResult(HealthCheckResult.Unhealthy($"{GetBotName()} is disconnected")),
                _ => Task.FromResult(HealthCheckResult.Unhealthy("Received an unknown state for the bot")),
            };

            string GetBotName()
                => $"{this.botSettings.Name} ({this.botSettings.Version})";
        }
    }
}
