// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Pandora.Utility;
using System;

namespace Link.Discord.Client
{
    internal sealed record DiscordBotUptime : IUptime<DiscordBotUptime>
    {
        public DateTimeOffset ElapsedDateTime { get; } = DateTimeOffset.Now;
    }
}
