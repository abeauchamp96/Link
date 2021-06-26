// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Bot;
using Link.Discord.Client.Extensions;
using Link.Server.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Pandora.Utility.Health;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppSettings()
    .UseSystemd();

ConfigureServices(builder);

await using var app = builder.Build();

ConfigureApp(app);

await app.RunAsync();

static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddHealthChecks()
        .AddCheck<BotHealthCheck>("bot", tags: new[] { "all", "bot" });

    builder.Services
        .AddDiscord(builder.Configuration.GetSection("bot:discord"));

    builder.Services.Configure<ConsoleLifetimeOptions>(o => o.SuppressStatusMessages = true)
        .Configure<BotSettings>(builder.Configuration.GetSection("bot"));
}

static void ConfigureApp(WebApplication app)
{
    var options = new HealthCheckOptions().UseModuleResponseWriter();

    app.UseHealthChecks("/health", CreateHealthCheckOptions(h => h.Tags.Contains("all")))
        .UseHealthChecks("/health/bot", CreateHealthCheckOptions(h => h.Tags.Contains("bot")));

    static HealthCheckOptions CreateHealthCheckOptions(Func<HealthCheckRegistration, bool> predicate)
        => new HealthCheckOptions() { Predicate = predicate }.UseModuleResponseWriter();
}
