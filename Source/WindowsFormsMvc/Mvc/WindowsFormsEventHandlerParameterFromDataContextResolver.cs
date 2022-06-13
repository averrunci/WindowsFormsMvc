// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Mvc;

internal sealed class WindowsFormsEventHandlerParameterFromDataContextResolver : EventHandlerParameterFromDataContextResolver
{
    public WindowsFormsEventHandlerParameterFromDataContextResolver(object? associatedElement) : base(associatedElement)
    {
    }

    protected override object? FindDataContext()
        => AssociatedElement is Control view ? view.FindWindowsFormsController()?.DataContextFinder.Find(view) : null;
}