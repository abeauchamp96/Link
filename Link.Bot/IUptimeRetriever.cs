// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using System;

namespace Link.Bot
{
    public interface IUptimeRetriever
    {
        string RetrieveFormattedUptime(DateTimeOffset currentTime);
    }
}
