﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace $safeprojectname$
{
    internal class $safeprojectname$ : IHostedService
    {
        private readonly I$safeprojectname$Application application;

        public $safeprojectname$(I$safeprojectname$Application application)
        {
            this.application = application ?? throw new ArgumentNullException(nameof(application));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            application.Run();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
