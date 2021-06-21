// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Bot.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppSettings()
    .UseSystemd();

ConfigureServices(builder);

await builder.Build().RunAsync();

static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.Configure<ConsoleLifetimeOptions>(o => o.SuppressStatusMessages = true);
}
