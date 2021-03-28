// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using System.Threading.Tasks;

namespace Link.Discord.Bots
{
    public interface IActivityUpdater
    {
        Task UpdateActivityAsync(DiscordActivity discordActivity);
    }
}
