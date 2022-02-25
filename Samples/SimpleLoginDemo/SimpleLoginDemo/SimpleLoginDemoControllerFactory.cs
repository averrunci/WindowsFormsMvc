﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Charites.Windows.Samples.SimpleLoginDemo;

public class SimpleLoginDemoControllerFactory : IWindowsFormsControllerFactory
{
    private readonly IServiceProvider services;

    public SimpleLoginDemoControllerFactory(IServiceProvider services) => this.services = services;

    public object Create(Type controllerType) => services.GetRequiredService(controllerType);
}