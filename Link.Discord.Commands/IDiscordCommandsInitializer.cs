// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using System.Threading.Tasks;

namespace Link.Discord.Commands
{
    public interface IDiscordCommandsInitializer
    {
        Task Initialize();
        Task Uninitialized();
    }
}
