﻿// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;

namespace Charites.Windows.Mvc;

internal sealed class WindowsFormsEventHandlerAction(
    MethodInfo method,
    object? target,
    IParameterDependencyResolver parameterDependencyResolver
) : EventHandlerAction(method, target, parameterDependencyResolver)
{
    protected override bool HandleUnhandledException(Exception exc) => WindowsFormsController.HandleUnhandledException(exc);
}