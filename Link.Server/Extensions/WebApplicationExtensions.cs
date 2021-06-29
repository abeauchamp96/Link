// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Pandora.Utility.Health;

namespace Link.Server.Extensions
{
    public static class WebApplicationExtensions
    {
        public static IApplicationBuilder UseAllHealthCheck(this IApplicationBuilder app)
            => app.UseHealthChecks("/health", CreateHealthCheckOptions("all"));
        public static IApplicationBuilder UseBotHealthCheck(this IApplicationBuilder app)
            => app.UseHealthChecks("/health/bot", CreateHealthCheckOptions("bot"));

        private static HealthCheckOptions CreateHealthCheckOptions(string tag)
            => new HealthCheckOptions() { Predicate = h => h.Tags.Contains(tag) }.UseModuleResponseWriter();
    }
}
