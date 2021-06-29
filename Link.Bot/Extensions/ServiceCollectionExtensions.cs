// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Bot.Health;
using Link.Bot.Helpers;
using Link.Bot.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Link.Bot.Tests")]
namespace Link.Bot.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBot(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddCheck<BotHealthCheck>("bot", tags: new[] { "all", "bot" });

            return services
                .Configure<BotSettings>(configuration)
                .AddTransient<IDelayHelper, DelayHelper>()
                .AddHostedService<BotRunner>();
        }
    }
}
