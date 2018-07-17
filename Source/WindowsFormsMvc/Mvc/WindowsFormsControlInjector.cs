// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Windows.Forms;

namespace Charites.Windows.Mvc
{
    internal sealed class WindowsFormsControlInjector : ElementInjector<Control>, IWindowsFormsControlInjector
    {
        protected override object FindElement(Control rootElement, string elementName)
            => rootElement.FindControl(elementName);
    }
}
