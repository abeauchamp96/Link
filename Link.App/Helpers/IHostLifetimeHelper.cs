// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using System;

namespace Link.App.Helpers
{
    public interface IHostLifetimeHelper
    {
        void RegisterApplicationStarted(Action callback);
        void RegisterApplicationStopped(Action callback);
        void RegisterApplicationStopping(Action callback);
    }
}
