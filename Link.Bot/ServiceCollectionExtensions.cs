// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Link.Bot.Tests")]
namespace Link.Bot
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureBot(this IServiceCollection services)
            => services.AddSingleton<IUptime>(s => new BotUptime(DateTimeOffset.Now));
    }
}
