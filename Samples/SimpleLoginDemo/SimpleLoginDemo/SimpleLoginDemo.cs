// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Charites.Windows.Samples.SimpleLoginDemo
{
    internal class SimpleLoginDemo : IHostedService
    {
        private readonly ISimpleLoginDemoApplication application;

        public SimpleLoginDemo(ISimpleLoginDemoApplication application)
        {
            this.application = application;
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