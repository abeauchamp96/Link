using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace Link.Tests.Helpers
{
    public static class LoggerMockExtensions
    {
        public static Mock<ILogger<T>> VerifyLogging<T>(this Mock<ILogger<T>> logger, string expectedMessage, LogLevel expectedLogLevel = LogLevel.Debug, Times? times = null)
        {
            times ??= Times.Once();

            logger.Verify(l => l.Log(
                expectedLogLevel,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => State(v, t, expectedMessage)),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), times.Value);

            return logger;
        }

        private static bool State(object v, Type t, string expectedMessage) => (v.ToString() ?? string.Empty).CompareTo(expectedMessage) == 0;
    }
}
