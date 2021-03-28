// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Link.Bot.Tests")]
namespace Link.Bot
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureBot(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<BotSettings>(configuration)
                .AddSingleton<IUptime>(s => new BotUptime(DateTimeOffset.Now))
                .AddTransient<IUptimeRetriever, UptimeRetriever>();
        }
    }
}
