// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Threading.Tasks;

namespace Link.Bot.Helpers
{
    internal sealed class DelayHelper : IDelayHelper
    {
        public Task DelayAsync(TimeSpan delay)
            => Task.Delay(delay);
    }
}
