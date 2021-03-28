// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord.Commands;
using Discord.WebSocket;
using Link.Discord.Bots.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Link.Discord.Bots.Tests")]
namespace Link.Discord.Bots
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiscord(this IServiceCollection services, IDiscordSettings discordSettings)
        {
            return services
                .AddSingleton(s => ConfigureDiscordSocketClient())
                .AddSingleton(s => ConfigureCommandService())
                .AddTransient<IActivityUpdater, ActivityUpdater>()
                .AddTransient<IDiscordClient, DiscordClient>();

            DiscordSocketClient ConfigureDiscordSocketClient() => new(new DiscordSocketConfig
            {
                MessageCacheSize = discordSettings.MessageCacheSize,
                LogLevel = discordSettings.LogLevel.ToLogLevel()
            });

            CommandService ConfigureCommandService() => new(new CommandServiceConfig
            {
                CaseSensitiveCommands = discordSettings.CaseSensitiveCommands,
                DefaultRunMode = discordSettings.CommandRunMode.ToRunMode(),
                LogLevel = discordSettings.LogLevel.ToLogLevel(),
                SeparatorChar = discordSettings.ArgumentSeparator
            });
        }
    }
}
