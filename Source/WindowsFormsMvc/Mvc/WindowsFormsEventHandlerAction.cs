﻿// Copyright (C) 2018-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Charites.Windows.Mvc
{
    internal sealed class WindowsFormsEventHandlerAction : EventHandlerAction
    {
        public WindowsFormsEventHandlerAction(MethodInfo method, object target) : base(method ?? throw new ArgumentNullException(nameof(method)), target)
        {
        }

        protected override bool HandleUnhandledException(Exception exc) => WindowsFormsController.HandleUnhandledException(exc);

        protected override IParameterDependencyResolver CreateParameterDependencyResolver(IDictionary<Type, Func<object>> dependencyResolver)
        {
            return new WindowsFormsParameterDependencyResolver(dependencyResolver);
        }
    }
}
