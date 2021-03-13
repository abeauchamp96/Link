// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.App.Helpers;
using Link.App.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pandora.Utility.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Link.App
{
    public sealed class AppMonitor : BackgroundService
    {
        private readonly IHostLifetimeHelper hostLifetimeHelper;
        private readonly ILogger<AppMonitor> logger;
        private readonly IDateTimeHelper dateTimeHelper;
        private readonly AppSettings appSettings;

        public AppMonitor(IHostLifetimeHelper hostLifetimeHelper, ILogger<AppMonitor> logger, IDateTimeHelper dateTimeHelper, IOptions<AppSettings> appSettings)
        {
            this.hostLifetimeHelper = hostLifetimeHelper;
            this.logger = logger;
            this.dateTimeHelper = dateTimeHelper;
            this.appSettings = appSettings.Value;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            this.hostLifetimeHelper.RegisterApplicationStarted(ApplicationStarted);
            this.hostLifetimeHelper.RegisterApplicationStopped(ApplicationStopped);

            await base.StartAsync(cancellationToken);

            void ApplicationStarted()
                => this.logger.LogInformation($"{this.appSettings.Name} [Version {this.appSettings.Version}] started");

            void ApplicationStopped()
                => this.logger.LogInformation($"{this.appSettings.Name} [Version {this.appSettings.Version}] stopped");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var delayInMinutes = TimeSpan.FromMinutes(1.0);

            while (!stoppingToken.IsCancellationRequested)
            {
                var now = this.dateTimeHelper.GetNow();

                this.logger.LogInformation($"{this.appSettings.Name} [Version {this.appSettings.Version}] is alive! - {now:yyyy-MM-dd HH:mm:ss}");
                await Task.Delay(delayInMinutes, stoppingToken);
            }
        }
    }
}
