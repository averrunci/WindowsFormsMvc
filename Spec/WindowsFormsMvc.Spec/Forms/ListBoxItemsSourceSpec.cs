// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms
{
    [Specification("ListBoxItemsSource Spec")]
    class ListBoxItemsSourceSpec
    {
        [Context]
        ListBoxItemsSourceSpec_ValueNotSpecified ValueNotSpecified => default!;

        [Context]
        ListBoxItemsSourceSpec_ValueSpecified ValueSpecified => default!;
    }
}
