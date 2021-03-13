// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.App.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Link.App
{
    internal static class Startup
    {
        public static async Task RunAsync()
        {
            using var host = BuildHost();

            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();

            Console.WriteLine(options.Value);

            await host.RunAsync();

            static IHost BuildHost()
            {
                return Host
                    .CreateDefaultBuilder(Array.Empty<string>())
                    .ConfigureServices(ConfigureServices)
                    .Build();

                static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
                {
                    services.Configure<AppSettings>(host.Configuration.GetSection("App"));

                    services.Configure<ConsoleLifetimeOptions>(o => o.SuppressStatusMessages = true);
                }
            }
        }
    }
}
