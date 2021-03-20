// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.App.Helpers;
using Link.App.Settings;
using Link.Tests.Helpers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Pandora.Utility.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Link.App
{
    public class AppMonitorTests
    {
        private readonly Mock<IHostLifetimeHelper> hostLifetimeHelperMock = new();
        private readonly Mock<ILogger<AppMonitor>> loggerMock = new();

        private readonly AppSettings appSettings;
        private readonly DateTime now = new(2021, 03, 13, 17, 20, 00);
        private readonly IHostedService service;

        public AppMonitorTests()
        {
            this.appSettings = new AppSettings { Name = "Ghost", Version = "0.1.0" };

            Mock<IOptions<AppSettings>> appSettingsMock = new();
            appSettingsMock.SetupGet(a => a.Value).Returns(this.appSettings);

            Mock<IDateTimeHelper> dateTimeHelperMock = new();
            dateTimeHelperMock.Setup(d => d.GetNow()).Returns(this.now);

            this.service = new AppMonitor(
                this.hostLifetimeHelperMock.Object,
                this.loggerMock.Object,
                dateTimeHelperMock.Object,
                appSettingsMock.Object);
        }

        [Fact]
        public async Task StartAsync_ShouldRegisterApplicationStarted()
        {
            await this.service.StartAsync(CancellationToken.None);

            this.hostLifetimeHelperMock.Verify(h => h.RegisterApplicationStarted(It.IsNotNull<Action>()));
        }

        [Fact]
        public async Task StartAsync_ShouldRegisterApplicationStopped()
        {
            await this.service.StartAsync(CancellationToken.None);

            this.hostLifetimeHelperMock.Verify(h => h.RegisterApplicationStopped(It.IsNotNull<Action>()));
        }

        [Fact]
        public async Task AppMonitor_ShouldMonitorUsingTheLogger()
        {
            await this.service.StartAsync(CancellationToken.None);

            this.loggerMock.VerifyLogging($"{this.appSettings.Name} [Version {this.appSettings.Version}] is alive! - {this.now:yyyy-MM-dd HH:mm:ss}", LogLevel.Information);
        }
    }
}
