// Copyright (C) 2022 Fievus
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
        ListViewItemContextSpec_BindChecked BindChecked => default!;

        [Context]
        ListViewItemContextSpec_BindText BindText => default!;

        [Context]
        ListViewItemContextSpec_BindBackColor BindBackColor => default!;

        [Context]
        ListViewItemContextSpec_BindForeColor BindForeColor => default!;

        [Context]
        ListViewItemContextSpec_BindFont BindFont => default!;
    }
}
