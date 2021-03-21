// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using System;

namespace Link.Bot
{
    internal sealed record BotUptime(DateTimeOffset ElapsedDateTime) : IUptime;
}
