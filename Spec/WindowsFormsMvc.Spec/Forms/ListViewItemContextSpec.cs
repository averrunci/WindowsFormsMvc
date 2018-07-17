// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms
{
    [Specification("ListViewItemContext Spec")]
    class ListViewItemContextSpec
    {
        [Context]
        ListViewItemContextSpec_BindChecked BindChecked { get; }

        [Context]
        ListViewItemContextSpec_BindText BindText { get; }

        [Context]
        ListViewItemContextSpec_BindBackColor BindBackColor { get; }

        [Context]
        ListViewItemContextSpec_BindForeColor BindForeColor { get; }

        [Context]
        ListViewItemContextSpec_BindFont BindFont { get; }
    }
}
