// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;

namespace Charites.Windows.Mvc;

internal sealed class WindowsFormsEventHandlerParameterFromDIResolver : EventHandlerParameterFromDIResolver
{
    public WindowsFormsEventHandlerParameterFromDIResolver(object? associatedElement) : base(associatedElement)
    {
    }

    protected override object? CreateParameter(Type parameterType)
        => (AssociatedElement as Component).FindWindowsFormsController()?.ControllerFactory.Create(parameterType);
}