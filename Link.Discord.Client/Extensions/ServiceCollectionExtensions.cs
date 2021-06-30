// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord.Commands;
using Discord.WebSocket;
using Link.Bot;
using Link.Discord.Commands;
using Link.Discord.Utility;
using Link.Discord.Utility.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pandora.Utility;

namespace Link.Discord.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiscord(this IServiceCollection services, IConfiguration configuration)
        {
            var discordSettings = configuration.Get<DiscordSettings>();

            return services
                .Configure<DiscordSettings>(configuration)
                .ConfigureDiscordClient(discordSettings)
                .ConfigureCommandService(discordSettings)
                .AddDiscordCommands()
                .AddUptime<DiscordBotUptime>()
                .AddSingleton<IBotConnector, DiscordBotConnector>();
        }

        private static IServiceCollection ConfigureDiscordClient(this IServiceCollection services, DiscordSettings discordSettings)
        {
            return services.AddSingleton(p => new DiscordSocketClient(new DiscordSocketConfig
            {
                MessageCacheSize = discordSettings.MessageCacheSize,
                LogLevel = discordSettings.LogLevel
            }));
        }

        private static IServiceCollection ConfigureCommandService(this IServiceCollection services, DiscordSettings discordSettings)
        {
            return services.AddSingleton(p => new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = discordSettings.CaseSensitiveCommands,
                DefaultRunMode = discordSettings.RunMode,
                LogLevel = discordSettings.LogLevel,
                SeparatorChar = discordSettings.ArgumentSeparator
            }));
        }
    }
}
