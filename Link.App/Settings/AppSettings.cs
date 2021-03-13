// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

namespace Link.App.Settings
{
    internal sealed record AppSettings
    {
        public string Name { get; init; } = string.Empty;

        public string Version { get; init; } = string.Empty;
    }
}
