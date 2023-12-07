// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Mvc;

internal sealed class WindowsFormsControllerTypeFinder(
    IWindowsFormsControlKeyFinder controlKeyFinder,
    IWindowsFormsDataContextFinder dataContextFinder
) : ControllerTypeFinder<Control>(controlKeyFinder, dataContextFinder), IWindowsFormsControllerTypeFinder
{
    protected override IEnumerable<Type> FindControllerTypeCandidates(Control view)
        => AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());
}