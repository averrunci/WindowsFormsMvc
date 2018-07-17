// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms
{
    [Specification("ComboBoxItemsSource Spec")]
    class ComboBoxItemsSourceSpec
    {
        [Context]
        ComboBoxItemsSourceSpec_ValueNotSpecified ValueNotSpecified { get; }

        [Context]
        ComboBoxItemsSourceSpec_ValueSpecified ValueSpecified { get; }
    }
}
