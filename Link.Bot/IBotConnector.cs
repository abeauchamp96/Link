// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using System.Threading.Tasks;

namespace Link.Bot
{
    public interface IBotConnector
    {
        BotConnectionState State { get; }

        Task ConnectAsync();
        Task DisconnectAsync();
    }
}
