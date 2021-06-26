// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Discord.WebSocket;
using Link.Discord.Client.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Link.Discord.Client.Extensions
{
    internal static class DiscordExtensions
    {
        public static IServiceCollection ConfigureDiscordClient(this IServiceCollection services, IConfiguration configuration)
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
