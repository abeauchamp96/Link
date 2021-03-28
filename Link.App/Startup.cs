// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.App.Helpers;
using Link.App.Settings;
using Link.Bot;
using Link.Discord.Bots;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Pandora.Utility;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Link.App.Tests")]
namespace Link.App
{
    internal static class Startup
    {
        public static async Task RunAsync()
        {
            using var host = BuildHost();

            await host.RunAsync();

            static IHost BuildHost()
            {
                return Host
                    .CreateDefaultBuilder(Array.Empty<string>())
                    .ConfigureServices(ConfigureServices)
                    .Build();

                static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
                {
                    services
                        .AddSingleton<IHostLifetimeHelper, HostLifetimeHelper>()
                        .AddDiscord(host.Configuration.GetSection("Discord"))
                        .AddBot(host.Configuration.GetSection("Bot"))
                        .AddHelpers();

                    services.AddHostedService<AppMonitor>();

                    services
                        .Configure<AppSettings>(host.Configuration.GetSection("App"))
                        .Configure<ConsoleLifetimeOptions>(o => o.SuppressStatusMessages = true);
                }
            }
        }
    }
}
