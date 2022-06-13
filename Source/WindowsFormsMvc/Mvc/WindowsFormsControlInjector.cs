// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;

namespace Charites.Windows.Mvc;

internal sealed class WindowsFormsControlInjector : ElementInjector<Component>, IWindowsFormsControlInjector
{
    public WindowsFormsControlInjector(IElementFinder<Component> elementFinder) : base(elementFinder)
    {
    }

    protected override object? FindElement(Component? rootElement, string elementName)
        => rootElement.FindComponent(elementName);
}