// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;

namespace Charites.Windows.Mvc;

internal sealed class WindowsFormsControlFinder : IWindowsFormsControlFinder
{
    public object? FindElement(Component? rootElement, string elementName)
        => rootElement.FindComponent(elementName);
}