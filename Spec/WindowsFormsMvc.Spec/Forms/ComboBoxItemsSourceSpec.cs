// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms;

[Specification("ComboBoxItemsSource Spec")]
class ComboBoxItemsSourceSpec
{
    [Context]
    ComboBoxItemsSourceSpec_ValueNotSpecified ValueNotSpecified => default!;

    [Context]
    ComboBoxItemsSourceSpec_ValueSpecified ValueSpecified => default!;
}