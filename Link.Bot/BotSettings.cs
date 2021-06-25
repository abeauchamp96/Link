// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using System;

namespace Link.Bot
{
    public record BotSettings
    {
        public string Name { get; init; } = string.Empty;
        public string Token { get; init; } = string.Empty;
        public string Version { get; init; } = string.Empty;
        public Uri? ProjectUrl { get; init; }
    }
}
