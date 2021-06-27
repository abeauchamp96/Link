// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord.WebSocket;
using Link.Bot;
using Link.Discord.Client.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pandora.Utility;

namespace Link.Discord.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiscord(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<DiscordSettings>(configuration)
                .ConfigureDiscordClient(configuration)
                .AddUptime<DiscordBotUptime>()
                .AddSingleton<IBotConnector, DiscordBotConnector>();
        }

        private static IServiceCollection ConfigureDiscordClient(this IServiceCollection services, IConfiguration configuration)
        {
            var discordSettings = configuration.Get<DiscordSettings>();

            return services.AddSingleton(p => new DiscordSocketClient(new DiscordSocketConfig
            {
                MessageCacheSize = discordSettings.MessageCacheSize,
                LogLevel = discordSettings.LogLevel
            }));
        }
    }
}
