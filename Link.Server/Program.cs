// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Bot.Extensions;
using Link.Discord.Client.Extensions;
using Link.Server.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppSettings()
    .UseSystemd();

ConfigureServices(builder);

await using var app = builder.Build();

ConfigureApp(app);

await app.RunAsync();

static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services
        .AddBot(builder.Configuration.GetSection("bot"))
        .AddDiscord(builder.Configuration.GetSection("bot:discord"));

    builder.Services.Configure<ConsoleLifetimeOptions>(o => o.SuppressStatusMessages = true);
}

static void ConfigureApp(WebApplication app)
{
    app.UseAllHealthCheck()
        .UseBotHealthCheck();
}
