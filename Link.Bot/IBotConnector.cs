// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

namespace Link.Bot
{
    public interface IBotConnector
    {
        BotConnectionState State { get; }

        void Connect(string token);
        void Disconnect();
    }
}
