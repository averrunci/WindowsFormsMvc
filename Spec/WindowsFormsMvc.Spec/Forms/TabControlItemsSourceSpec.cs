// Copyright (C) 2018 Fievus
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
        TabControlItemsSourceSpec_HeaderNotSpecified HeaderNotSpecified { get; }

        [Context]
        TabControlItemsSourceSpec_HeaderMemberSpecified HeaderMemberSpecified { get; }

        [Context]
        TabControlItemsSourceSpec_HeaderSpecified HeaderSpecified { get; }
    }
}
