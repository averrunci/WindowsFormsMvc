// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Charites.Windows.Mvc
{
    internal sealed class WindowsFormsControllerTypeFinder : ControllerTypeFinder<Control>, IWindowsFormsControllerTypeFinder
    {
        public WindowsFormsControllerTypeFinder(IWindowsFormsControlKeyFinder controlKeyFinder, IWindowsFormsDataContextFinder dataContextFinder) : base(controlKeyFinder, dataContextFinder)
        {
        }

        protected override IEnumerable<Type> FindControllerTypeCandidates(Control view)
            => AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());
    }
}
