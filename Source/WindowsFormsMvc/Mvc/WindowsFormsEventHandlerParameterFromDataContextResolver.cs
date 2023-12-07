// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Mvc;

internal sealed class WindowsFormsEventHandlerParameterFromDataContextResolver(object? associatedElement) : EventHandlerParameterFromDataContextResolver(associatedElement)
{
    protected override object? FindDataContext()
        => AssociatedElement is Control view ? view.FindWindowsFormsController()?.DataContextFinder.Find(view) : null;
}