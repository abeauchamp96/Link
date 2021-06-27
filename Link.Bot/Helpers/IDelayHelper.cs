// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Threading.Tasks;

namespace Link.Bot.Helpers
{
    public interface IDelayHelper
    {
        Task DelayAsync(TimeSpan delay);
    }
}
