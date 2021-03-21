// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using FluentAssertions;
using System;
using Xunit;

namespace Link.Bot
{
    public class BotUptimeTests
    {
        [Fact]
        public void Constructor_ShouldInitializeTheProperties()
        {
            var now = DateTimeOffset.Now;

            BotUptime uptime = new(now);

            uptime.ElapsedDateTime.Should().Be(now);
        }
    }
}
