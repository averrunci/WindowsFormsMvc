// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms
{
    [Specification("TabControlItemsSource Spec")]
    class TabControlItemsSourceSpec
    {
        [Context]
        TabControlItemsSourceSpec_HeaderNotSpecified HeaderNotSpecified => default!;

        [Context]
        TabControlItemsSourceSpec_HeaderMemberSpecified HeaderMemberSpecified => default!;

        [Context]
        TabControlItemsSourceSpec_HeaderSpecified HeaderSpecified => default!;
    }
}
