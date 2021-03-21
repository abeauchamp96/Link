// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using NodaTime;
using NodaTime.Extensions;
using System;
using System.Linq;

namespace Link.Bot
{
    internal sealed class UptimeRetriever : IUptimeRetriever
    {
        private readonly IUptime uptime;

        public UptimeRetriever(IUptime uptime) => this.uptime = uptime;

        public string RetrieveFormattedUptime(DateTimeOffset currentTime)
        {
            return CanNotRetrieveUptime()
                ? "Can't retrieve the uptime"
                : GetFormattedTime();

            bool CanNotRetrieveUptime()
            {
                return IsUptimeNotValid() || IsCurrentTimeNotValid();

                bool IsCurrentTimeNotValid()
                    => currentTime == DateTimeOffset.MinValue || currentTime < this.uptime.ElapsedDateTime;

                bool IsUptimeNotValid()
                    => this.uptime.ElapsedDateTime == DateTimeOffset.MinValue;
            }

            string GetFormattedTime()
            {
                var period = GetPeriodBetweenElapsedDatetimeAndCurrentTime();

                return new[]
                {
                    FormatTimeUnitOrEmpty(period.Years, nameof(period.Years)),
                    FormatTimeUnitOrEmpty(period.Months, nameof(period.Months)),
                    FormatTimeUnitOrEmpty(period.Days, nameof(period.Days)),
                    FormatTimeUnitOrEmpty(period.Hours, nameof(period.Hours)),
                    FormatTimeUnitOrEmpty(period.Minutes, nameof(period.Minutes)),
                    FormatTimeUnitOrEmpty(period.Seconds, nameof(period.Seconds))
                }
                .Where(unit => !string.IsNullOrEmpty(unit))
                .Aggregate((currentFormat, nextUnit) => $"{currentFormat}, {nextUnit}");

                static string FormatTimeUnitOrEmpty(long time, string unit)
                    => time != 0 ? $"{time:D2} {unit}" : string.Empty;

                Period GetPeriodBetweenElapsedDatetimeAndCurrentTime()
                {
                    var now = currentTime.ToOffsetDateTime();
                    var then = this.uptime.ElapsedDateTime.ToOffsetDateTime();

                    return Period.Between(then.LocalDateTime, now.LocalDateTime);
                }
            }
        }
    }
}
