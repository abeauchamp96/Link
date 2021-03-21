// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace Link.Bot
{
    public class UptimeRetrieverTests
    {
        private readonly Mock<IUptime> uptimeMock = new();

        private readonly IUptimeRetriever retriever;

        public UptimeRetrieverTests()
        {
            this.uptimeMock.SetupGet(u => u.ElapsedDateTime).Returns(DateTimeOffset.Now);

            this.retriever = new UptimeRetriever(this.uptimeMock.Object);
        }

        [Fact]
        public void RetrieveFormattedUptime_ShouldReturnAnErrorMessage_WhenCurrentDateIsEqualToMinValue()
        {
            var uptime = this.retriever.RetrieveFormattedUptime(DateTimeOffset.MinValue);

            uptime.Should().Be("Can't retrieve the uptime");
        }

        [Fact]
        public void RetrieveFormattedUptime_ShouldReturnAnErrorMessage_WhenCurrentTimeIsLessThanUptime()
        {
            this.uptimeMock.SetupGet(u => u.ElapsedDateTime).Returns(DateTimeOffset.Now.AddDays(1));

            var uptime = this.retriever.RetrieveFormattedUptime(DateTimeOffset.Now);

            uptime.Should().Be("Can't retrieve the uptime");
        }

        [Fact]
        public void RetrieveFormattedUptime_ShouldReturnAnErrorMessage_WhenElapsedTimeIsEqualToMinValue()
        {
            this.uptimeMock.SetupGet(u => u.ElapsedDateTime).Returns(DateTimeOffset.MinValue);

            var uptime = this.retriever.RetrieveFormattedUptime(DateTimeOffset.Now);

            uptime.Should().Be("Can't retrieve the uptime");
        }

        [Theory]
        [MemberData(nameof(GetFormattedUptimes))]
        public void RetrieveFormattedUptime_ShouldRetrieveTheFormattedUptime(DateTimeOffset elapsedDatetime, string expectedFormattedUptime)
        {
            DateTimeOffset currentTime = new(new(2021, 12, 25, 11, 00, 00));

            this.uptimeMock.SetupGet(u => u.ElapsedDateTime).Returns(elapsedDatetime);

            var uptime = this.retriever.RetrieveFormattedUptime(currentTime);

            uptime.Should().BeEquivalentTo(expectedFormattedUptime);
        }

        public static TheoryData<DateTimeOffset, string> GetFormattedUptimes() => new()
        {
            { new(new DateTime(2021, 12, 25, 10, 59, 40)), "20 Seconds" },
            { new(new DateTime(2021, 12, 25, 10, 27, 00)), "33 Minutes" },
            { new(new DateTime(2021, 12, 24, 23, 00, 00)), "12 Hours" },
            { new(new DateTime(2021, 12, 24, 06, 00, 00)), "01 Days, 05 Hours" },
            { new(new DateTime(2021, 11, 25, 06, 00, 00)), "01 Months, 05 Hours" },
            { new(new DateTime(2020, 12, 25, 06, 00, 00)), "01 Years, 05 Hours" },
            { new(new DateTime(2021, 11, 24, 06, 00, 00)), "01 Months, 01 Days, 05 Hours" },
            { new(new DateTime(2020, 11, 25, 06, 00, 00)), "01 Years, 01 Months, 05 Hours" },
            { new(new DateTime(2020, 12, 24, 06, 00, 00)), "01 Years, 01 Days, 05 Hours" },
            { new(new DateTime(2019, 10, 20, 07, 30, 00)), "02 Years, 02 Months, 05 Days, 03 Hours, 30 Minutes" },
            { new(new DateTime(2021, 11, 25, 10, 30, 00)), "01 Months, 30 Minutes" }
        };
    }
}
