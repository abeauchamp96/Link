// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using System;

namespace Link.Bot
{
    public sealed record BotSettings
    {
        public string Name { get; init; } = string.Empty;
        public char Prefix { get; init; }
        public string Token { get; init; } = string.Empty;
        public string Version { get; init; } = string.Empty;
        public Uri? ProjectUrl { get; init; }
    }
}
