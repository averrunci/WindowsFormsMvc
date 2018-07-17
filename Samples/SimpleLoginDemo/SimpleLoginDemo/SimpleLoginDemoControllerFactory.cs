// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Adapter;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation;
using Microsoft.Extensions.DependencyInjection;

namespace Charites.Windows.Samples.SimpleLoginDemo
{
    public class SimpleLoginDemoControllerFactory : IWindowsFormsControllerFactory
    {
        private readonly IServiceProvider services = new ServiceCollection()
            .AddControllers()
            .AddPresentationAdapters()
            .BuildServiceProvider();

        public object Create(Type controllerType) => services.GetRequiredService(controllerType);
    }
}
