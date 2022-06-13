// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;

namespace Charites.Windows.Mvc;

internal sealed class WindowsFormsEventHandlerParameterFromElementResolver : EventHandlerParameterFromElementResolver
{
    public WindowsFormsEventHandlerParameterFromElementResolver(object? associatedElement) : base(associatedElement)
    {
    }

    protected override object? FindElement(string name)
        => AssociatedElement is Component component ? component.FindWindowsFormsController()?.ControlFinder.FindElement(component, name) : null;
}