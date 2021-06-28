// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;

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

        public static IHostBuilder ConfigureCertificate(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureWebHost(h => h.UseUrls().UseKestrel((host, options) =>
            {
                if (host.HostingEnvironment.IsDevelopment())
                    return;

                var httpsPort = host.Configuration.GetValue<int>("httpsPort");
                var certificateName = host.Configuration.GetValue<string>("certificate");

                options.Listen(IPAddress.Loopback, httpsPort, options =>
                {
                    options.UseHttps(StoreName.Root, certificateName);
                });
            }));
        }
    }
}
