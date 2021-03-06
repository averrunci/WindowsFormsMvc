﻿// Copyright (C) 2020-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading.Tasks;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Adapter;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Charites.Windows.Samples.SimpleLoginDemo
{
    internal static class Program
    {
        [STAThread]
        private static async Task Main(string[] args)
            => await CreateHostBuilder().Build().RunAsync();

        private static IHostBuilder CreateHostBuilder()
            => Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => ConfigureServices(context.Configuration, services));

        private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
            => services.AddHostedService<SimpleLoginDemo>()
                .AddSingleton<IWindowsFormsControllerFactory, SimpleLoginDemoControllerFactory>()
                .AddSingleton<ISimpleLoginDemoApplication, SimpleLoginDemoApplication>()
                .AddSingleton<IContentNavigator, ContentNavigator>(
                    p =>
                    {
                        IContentNavigator navigator = new ContentNavigator();
                        navigator.IsNavigationStackEnabled = false;
                        return (ContentNavigator)navigator;
                    })
                .AddControllers()
                .AddPresentationAdapters();
    }
}
