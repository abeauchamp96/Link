// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Link.App
{
    public static class Startup
    {
        public static async Task RunAsync()
        {
            using var host = BuildHost();

            await host.RunAsync();

            static IHost BuildHost()
                => Host.CreateDefaultBuilder(Array.Empty<string>())
                    .Build();
        }
    }
}
