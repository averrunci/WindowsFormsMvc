// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;

namespace Charites.Windows.Mvc;

internal sealed class WindowsFormsControlInjector(IElementFinder<Component> elementFinder) : ElementInjector<Component>(elementFinder), IWindowsFormsControlInjector
{
    protected override object? FindElement(Component? rootElement, string elementName)
        => rootElement.FindComponent(elementName);
}