// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord.Commands;
using Discord.WebSocket;
using Link.Discord.Bots.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Link.Discord.Bots.Tests")]
namespace Link.Discord.Bots
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiscord(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<DiscordSettings>(configuration)
                .AddSingleton(s => ConfigureDiscordSocketClient(s.GetRequiredService<IOptions<DiscordSettings>>()))
                .AddSingleton(s => ConfigureCommandService(s.GetRequiredService<IOptions<DiscordSettings>>()))
                .AddTransient<IActivityUpdater, ActivityUpdater>()
                .AddTransient<IDiscordClient, DiscordClient>();

            DiscordSocketClient ConfigureDiscordSocketClient(IOptions<DiscordSettings> discordSettings) => new(new DiscordSocketConfig
            {
                MessageCacheSize = discordSettings.Value.MessageCacheSize,
                LogLevel = discordSettings.Value.LogLevel.ToLogLevel()
            });

            CommandService ConfigureCommandService(IOptions<DiscordSettings> discordSettings) => new(new CommandServiceConfig
            {
                CaseSensitiveCommands = discordSettings.Value.CaseSensitiveCommands,
                DefaultRunMode = discordSettings.Value.CommandRunMode.ToRunMode(),
                LogLevel = discordSettings.Value.LogLevel.ToLogLevel(),
                SeparatorChar = discordSettings.Value.ArgumentSeparator
            });
        }
    }
}
