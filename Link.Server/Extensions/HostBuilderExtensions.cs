// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Link.Server.Extensions
{
    internal static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureAppSettings(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureAppConfiguration(ConfigureDevelopmentSettings);

            static void ConfigureDevelopmentSettings(HostBuilderContext hostBuilderContext, IConfigurationBuilder configurationBuilder)
            {
                if (!hostBuilderContext.HostingEnvironment.IsDevelopment())
                    return;

                var path = Path.GetFullPath("appsettings.Development.json");
                configurationBuilder.AddJsonFile(path);
            }
        }
    }
}
