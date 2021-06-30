// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Discord.Commands.Bot;
using Link.Discord.Commands.Misc;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Link.Discord.Commands.Tests")]
namespace Link.Discord.Commands
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiscordCommands(this IServiceCollection services)
        {
            return services
                .AddSingleton<IDiscordCommandsInitializer, DiscordCommandsInitializer>()
                .AddTransient<IBotCommands, BotCommands>()
                .AddTransient<IMiscCommands, MiscCommands>();
        }
    }
}
