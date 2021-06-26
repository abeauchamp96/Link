// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Bot;
using Link.Discord.Client.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Link.Discord.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiscord(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<DiscordSettings>(configuration)
                .ConfigureDiscordClient(configuration)
                .AddTransient<IBotConnector, DiscordBotConnector>();
        }
    }
}
