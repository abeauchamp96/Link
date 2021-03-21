// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Link.Bot
{
    public class BotRunner : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken) => throw new System.NotImplementedException();
    }
}
