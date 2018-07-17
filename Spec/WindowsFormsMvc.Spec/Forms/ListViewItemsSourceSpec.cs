// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms
{
    [Specification("ListViewItemsSource Spec")]
    class ListViewItemsSourceSpec
    {
        [Context]
        ListViewItemsSourceSpec_ItemBound ItemBound { get; }

        [Context]
        ListViewItemsSourceSpec_CheckedMemberSpecified CheckedMemberSpecified { get; }

        [Context]
        ListViewItemsSourceSpec_CheckedSpecified CheckedSpecified { get; }
    }
}
