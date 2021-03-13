// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Extensions.Hosting;
using System;

namespace Link.App.Helpers
{
    internal sealed class HostLifetimeHelper : IHostLifetimeHelper
    {
        private readonly IHostApplicationLifetime hostApplicationLifetime;

        public HostLifetimeHelper(IHostApplicationLifetime hostApplicationLifetime)
            => this.hostApplicationLifetime = hostApplicationLifetime;

        public void RegisterApplicationStarted(Action callback)
            => this.hostApplicationLifetime.ApplicationStarted.Register(callback);

        public void RegisterApplicationStopped(Action callback)
            => this.hostApplicationLifetime.ApplicationStopped.Register(callback);

        public void RegisterApplicationStopping(Action callback)
            => this.hostApplicationLifetime.ApplicationStopping.Register(callback);
    }
}
